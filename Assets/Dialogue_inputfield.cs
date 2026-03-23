using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class Dialogue_inputfield : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text outputText;
    [TextArea(3, 10)]
    public string systemPrompt;
    private string url = "http://127.0.0.1:11434/api/generate";

    public void SendRequest()
    {
        StartCoroutine(PostRequest(inputField.text));
        inputField.text = "";
    }

    IEnumerator PostRequest(string prompt)
    {
        outputText.text = "NPC is thinking...";
        
        string json = "{\"model\": \"phi3:latest\", \"system\": \"" + systemPrompt + "\", \"prompt\": \"" + prompt + "\", \"stream\": false}";
        
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            Debug.Log("Test");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<OllamaResponse>(request.downloadHandler.text);
                outputText.text = response.response;
            }
            else
            {
                outputText.text = "Error: " + request.error;
            }
        }
    }
}

[System.Serializable]
public class OllamaResponse { public string response; }
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class OllamaConnector : MonoBehaviour
{
    private string url = "http://127.0.0.1:11434/api/generate";

    [TextArea(3, 10)]
    public string systemPrompt;

    public IEnumerator AskNPC(string prompt, System.Action<string> onWordReceived)
    {
        string json = "{\"model\": \"phi3:latest\", \"system\": \"" + systemPrompt + "\", \"prompt\": \"" + prompt + "\", \"stream\": true}";
        
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new StreamDownloadHandler(onWordReceived);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
        }
    }
}

public class StreamDownloadHandler : DownloadHandlerScript
{
    private System.Action<string> callback;
    public StreamDownloadHandler(System.Action<string> cb) : base(new byte[1024]) => callback = cb;

    protected override bool ReceiveData(byte[] data, int dataLength)
    {
        string jsonChunk = Encoding.UTF8.GetString(data, 0, dataLength);
        Debug.Log("Chunk received: " + jsonChunk); 
        return true;
    }
}
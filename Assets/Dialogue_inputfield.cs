using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System;

public class Dialogue_inputfield : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text outputText;
    [TextArea(3, 10)]
    public string systemPrompt;
    List<string> history;
    private string url = "http://127.0.0.1:11434/api/generate";
    public DialogueTrigger trigger;
    public NPCsystem action;

    private void Start()
    {
        trigger = FindAnyObjectByType<DialogueTrigger>();
        history = trigger.dialogue.sentences;

        trigger.dialogue.sentences = new List<string>();
    } 

    public void SendRequest()
    {
        if (string.IsNullOrWhiteSpace(inputField.text)) 
        {
            Debug.Log("No text provided -- skipped.");
            return;
        }
        
        trigger.dialogue.sentences.Add(inputField.text);
        Debug.Log(trigger.dialogue.sentences.Count);
        StartCoroutine(PostRequest(inputField.text));
        inputField.text = "";
    }

    IEnumerator PostRequest(string prompt)
    {
        trigger.dialogue.sentences.Add("NPC is thinking...");
        FindAnyObjectByType<DialogueManager>().UpdateDialogue(trigger.dialogue);
        //outputText.text = "NPC is thinking...";
        history.Add("User: " + prompt + "\n");
        string fullPrompt = String.Join("\n", history) + "AI:";
        //fullPrompt = fullPrompt.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("\r", "\\r");
        //systemPrompt = systemPrompt.Replace("\n", "\\n").Replace("\"", "\\\"").Replace("\r", "\\r");

        OllamaRequest req = new OllamaRequest();
        req.model = "phi3:latest";
        req.system = systemPrompt;
        req.prompt = fullPrompt;
        req.stream = false;
        req.options = new Options { stop = new string[] { "User:", "AI:", "\n" } };

        string json = JsonUtility.ToJson(req);      
        Debug.Log(json);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            //Debug.Log("Test");
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<OllamaResponse>(request.downloadHandler.text);
                //outputText.text = response.response;
                trigger.dialogue.sentences.RemoveAt(trigger.dialogue.sentences.IndexOf("NPC is thinking..."));
                trigger.dialogue.sentences.Add(response.response);
                Debug.Log(response.response);
                history.Add("AI: " + response.response + "\n");
                FindAnyObjectByType<DialogueManager>().UpdateDialogue(trigger.dialogue);
            }
            else
            {
                trigger.dialogue.sentences.RemoveAt(trigger.dialogue.sentences.IndexOf("NPC is thinking..."));
                trigger.dialogue.sentences.Add("Error: " + request.error);
            }
        }
    }
}

[System.Serializable]
public class OllamaResponse { public string response; }

[System.Serializable]
public class OllamaRequest {
    public string model;
    public string system;
    public string prompt;
    public bool stream;
    public Options options;
}

[System.Serializable]
public class Options {
    public string[] stop;
}
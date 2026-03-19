using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public TextMeshProUGUI nametext;
    public TextMeshProUGUI dialoguetext;

    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    } 

    public void StartDialogue (Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);
        FirstPersonController.dialogue = true;
        animator.SetBool("IsOpen", true);

        nametext.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialoguetext.text = sentence;
        //Debug.Log(sentence);
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        FirstPersonController.dialogue = false;
    }
}

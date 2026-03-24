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

    private LinkedList<string> sentences;
    private LinkedListNode<string> sentence;
    private int count;

    void Start()
    {
        sentences = new LinkedList<string>();
    } 

    public void StartDialogue (Dialogue dialogue)
    {
        count = 0;
        //Debug.Log("Starting conversation with " + dialogue.name);
        FirstPersonController.dialogue = true;
        animator.SetBool("IsOpen", true);

        nametext.text = dialogue.name;

        sentences.Clear();
        //Example: 1 2 3 4 5
        foreach (string sentence in dialogue.sentences)
        {
            sentences.AddLast(sentence);
        }
        sentence = sentences.First;
        dialoguetext.text = sentence.Value;
        //DisplayNextSentence();
        Debug.Log("Full count: " + sentences.Count);
    }

    public void DisplayNextSentence()
    {
        if (count == sentences.Count-1)
        {
            EndDialogue();
            return;
        }
        else count++;
        //Debug.Log(count);
        //The list before: 1 2 3 4 5
        //Here I take the first (1) node, 
        // I remove it from the list and add it as a last (1 instead of 5) node, 
        // but I take the first (2) to write down 
        //The list after: 2 3 4 5 1
        sentence = sentences.First;
        sentences.RemoveFirst();
        sentences.AddLast(sentence);
        sentence = sentences.First;
        dialoguetext.text = sentence.Value;
        //Debug.Log(sentence);
    }
    public void DisplayPreviousSentence()
    {
        if (count > 0)
        {
            count--;  
            //Debug.Log(count);
            //For example you moved from 2 to 3 and want to go back to 2
            //The list before: 3 4 5 1 2
            //Here I take the last (2) node, 
            // I remove it and add it as a first (2 instead of 3) node,
            //but I also take first (2) to write down.
            //If I used last (1) node but not first (2) the transition would be wrong (3 isn't after 1 but after 2)
            //The list after: 2 3 4 5 1 
            sentence = sentences.Last;
            sentences.RemoveLast();
            sentences.AddFirst(sentence);
            sentence = sentences.First;
            dialoguetext.text = sentence.Value;
            //Debug.Log(sentence);
        } 
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        FirstPersonController.dialogue = false;
    }
}

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

    private LinkedList<string> dsentences;
    private LinkedListNode<string> dsentence;
    private int count;

    void Start()
    {
        dsentences = new LinkedList<string>();
    } 

    public void StartDialogue (Dialogue dialogue)
    {
        count = 0;
        //Debug.Log("Starting conversation with " + dialogue.name);
        FirstPersonController.dialogue = true;
        animator.SetBool("IsOpen", true);

        nametext.text = dialogue.name;

        dsentences.Clear();
        //Example: 1 2 3 4 5
        foreach (string sentence in dialogue.sentences)
        {
            dsentences.AddLast(sentence);
        }
        dsentence = dsentences.First;
        if (dsentences.Count != 0) dialoguetext.text = dsentence.Value;
        else dialoguetext.text = "";
        //DisplayNextSentence();
        Debug.Log("Full count: " + dsentences.Count);
    }

    public void UpdateDialogue (Dialogue dialogue)
    {
        count = 0;
        dsentences.Clear();
        //Example: 1 2 3 4 5
        foreach (string sentence in dialogue.sentences)
        {
            dsentences.AddLast(sentence);
        }
        dsentence = dsentences.First;
        dialoguetext.text = dsentence.Value;
        //DisplayNextSentence();
        Debug.Log("Full count: " + dsentences.Count);
    }

    public void DisplayNextSentence()
    {
        if (count == dsentences.Count-1 || dsentences.Count == 0)
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
        dsentence = dsentences.First;
        dsentences.RemoveFirst();
        dsentences.AddLast(dsentence);
        dsentence = dsentences.First;
        dialoguetext.text = dsentence.Value;
        //Debug.Log(sentence);
    }
    public void DisplayPreviousSentence()
    {
        if (dsentences.Count == 0) return;
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
            dsentence = dsentences.Last;
            dsentences.RemoveLast();
            dsentences.AddFirst(dsentence);
            dsentence = dsentences.First;
            dialoguetext.text = dsentence.Value;
            //Debug.Log(sentence);
        } 
    }

    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
        var inputFieldScript = FindAnyObjectByType<Dialogue_inputfield>();
        inputFieldScript.inputField.interactable = false;

        FirstPersonController.dialogue = false;
    }
}

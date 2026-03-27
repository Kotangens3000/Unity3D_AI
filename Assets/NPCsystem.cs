using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NPCsystem : MonoBehaviour
{
    public GameObject myInput;
    public bool stareatplayer = true;
    private Transform player;
    private bool player_detection = false;
    public bool LLM_active = true;

    void Update()
    {
        if(player_detection)
        {
            //the NPC stares at the player anyway
            if (player != null && stareatplayer)
            {
                // Smooth rotating towards the player
                Vector3 direction = player.position - transform.position;
                direction.y = 0; // the NPC won't stare while the player's above/below it
                
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
            //only when F is pressed, show up a dialogue
            if (Input.GetKeyDown(KeyCode.F) && !FirstPersonController.dialogue)
            {
                //If we want to make our NPC use LLM, enable input field, otherwise don't
                //and when we go out of dialogue/don't use LLM, 
                // we don't need it, and such, turn it off completely
                if (LLM_active) 
                {
                    myInput.SetActive(true);
                    var inputFieldScript = FindAnyObjectByType<Dialogue_inputfield>();
                    inputFieldScript.inputField.interactable = true;
                } 
                else myInput.SetActive(false);
                
                Debug.Log("Dialogue started");
                var dialtri = GetComponent<DialogueTrigger>(); //new DialogueTrigger() is not allowed, thus, error => no custom text/name
                dialtri.TriggerDialogue();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PlayerBody") // our player 
        {
            player = other.transform;
            player_detection = true;
            
            var inputFieldScript = FindAnyObjectByType<Dialogue_inputfield>();
            
            if (inputFieldScript != null)
            {
                inputFieldScript.action = this;
                Debug.Log("You are with " + gameObject.name);
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        player = null;

        var inputFieldScript = FindAnyObjectByType<Dialogue_inputfield>();
        inputFieldScript.action = null;
    }

}

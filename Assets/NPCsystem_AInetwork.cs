using UnityEngine;
public class NPCsystem_AInetwork : MonoBehaviour
{
    private OllamaConnector ollama;
    public GameObject myInput;
    public bool stareatplayer = true;
    private Transform player;
    bool player_detection = false;

    void Start()
    {
        ollama = gameObject.AddComponent<OllamaConnector>();
    }

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
            //As it is AI, it uses LLM for dialogues
            if (Input.GetKeyDown(KeyCode.F) && !FirstPersonController.dialogue)
            {
                myInput.SetActive(true);
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
        player = null;
    }

}

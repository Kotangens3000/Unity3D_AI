using UnityEngine;

public class Button_update_terrain : MonoBehaviour, IInteractable
{
    public MeshGenerator generator;

    public void Start()
    {
        generator = Object.FindAnyObjectByType<MeshGenerator>();
    }
    public void Interact()
    {
        Debug.Log("Updated");
        generator.RandomOffset();
        generator.CreateShape();
        generator.UpdateMesh();
        
    }
}

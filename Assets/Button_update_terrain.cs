using UnityEngine;

public class Button_update_terrain : MonoBehaviour, IInteractable
{
    public MeshGenerator generator;

    public void Start()
    {
        generator = Object.FindFirstObjectByType<MeshGenerator>();
    }
    public void Interact()
    {
        Debug.Log("Updated");
        generator.CreateShape();
        generator.UpdateMesh();
        
    }
}

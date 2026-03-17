using UnityEngine;
using System.Collections;
using System.Collections.Generic;

interface IInteractable
{
    void Interact();
}

public class Iteractor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;

    void Start()
    {
        
    }

    void Update ()
    {
        if (InteractorSource == null) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            Ray r = new Ray(InteractorSource.position, InteractorSource.forward);

            Debug.DrawRay(r.origin, r.direction * InteractRange, Color.red, 1f);

            if (Physics.Raycast(r, out RaycastHit hitinfo, InteractRange))
            {
                if (hitinfo.collider.gameObject.TryGetComponent(out IInteractable interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }
}

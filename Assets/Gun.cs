using UnityEngine.Events;
using UnityEngine;
using Unity.ProjectAuditor.Editor.Core;

public class Gun : MonoBehaviour
{
    [SerializeField] private AudioClip sound_shoot;
    private AudioSource audioSource;
    public UnityEvent OnGunShoot;
    public float FireCooldown;
    private float CurrentCooldown;

    void Start()
    {
        CurrentCooldown = FireCooldown;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentCooldown <= 0f && !FirstPersonController.dialogue)
            {
                OnGunShoot?.Invoke();
                CurrentCooldown = FireCooldown;
                
                audioSource.PlayOneShot(sound_shoot);

            }
        }
        if (CurrentCooldown > 0f) CurrentCooldown -= Time.deltaTime;
    }
}

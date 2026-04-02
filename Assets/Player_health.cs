using System;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    [SerializeField] private AudioClip sound_dead;
    private AudioSource audioSource;
    public int maxHealth = 100;
    public int currentHealth;

    public TMPro.TextMeshProUGUI Health_UI;
    void Start()
    {
        currentHealth = maxHealth;
        Health_UI.text = Convert.ToString(currentHealth);

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // intentionally make our player unkillable
        if (currentHealth <= 0)
        {
            Debug.Log("Dead");
            audioSource.PlayOneShot(sound_dead);
            currentHealth = maxHealth;
            Health_UI.text = Convert.ToString(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Health_UI.text = Convert.ToString(currentHealth);
    }
}

using System;
using UnityEngine;

public class Player_health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public TMPro.TextMeshProUGUI Health_UI;
    void Start()
    {
        currentHealth = maxHealth;
        Health_UI.text = Convert.ToString(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Health_UI.text = Convert.ToString(currentHealth);
    }
}

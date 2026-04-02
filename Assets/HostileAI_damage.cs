using UnityEngine;

public class HostileAI_damage : MonoBehaviour
{
    private bool didhit = false;
    public int damage = 10;
    private void OnCollisionEnter(Collision collision)
    {
        if (didhit) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            didhit = true;

            Player_health player_Health = collision.gameObject.GetComponent<Player_health>();
            player_Health.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}

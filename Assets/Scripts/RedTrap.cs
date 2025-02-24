using UnityEngine;

public class RedTrap : MonoBehaviour
{
    [Tooltip("Damage dealt to the player when triggered.")]
    public int damageAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
         
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.LoseHealth(damageAmount);
            }
        }
    }
}

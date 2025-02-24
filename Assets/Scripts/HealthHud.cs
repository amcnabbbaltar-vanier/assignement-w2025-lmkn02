using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthHUD : MonoBehaviour
{
    [Tooltip("Assign the PlayerHealth script from the Player in the Inspector")]
    public PlayerHealth playerHealth;

    [Tooltip("Assign the UI Text (or TMP) that displays health")]
  
   public TextMeshProUGUI healthText;

    private void Update()
    {
        if (playerHealth != null && healthText != null)
        {
            healthText.text = "Health: " + playerHealth.GetCurrentHealth();
        }
    }
}

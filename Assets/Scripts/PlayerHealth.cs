using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 3;  
    private int currentHealth;

    [Header("Fall Reset Threshold")]
    [Tooltip("If the player's Y-position goes below this, the level resets and health -1.")]
    public float fallThreshold = -10f;

    private void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Player starts with " + currentHealth + " HP.");
    }

    private void Update()
    {
       
        if (transform.position.y < fallThreshold)
        {
            LoseHealth(1);
            ResetLevel();
        }
    }

    
    public void LoseHealth(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player damaged. Current Health = " + currentHealth);

     
        if (currentHealth <= 0)
        {
            Debug.Log("Health is 0! Restarting level...");
            ResetLevel();
       
            currentHealth = maxHealth;
        }
    }

 
    public void ResetLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

   
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}

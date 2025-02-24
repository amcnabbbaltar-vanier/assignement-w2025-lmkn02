using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Score & time
    public int currentScore = 0;
    public float totalTime = 0f;       // Tracks time across updates
    public float finalTime = 0f;       // Stores a "final" time (e.g., when level ends)

    // UI reference
    public GameObject clockUI;

    private void Awake()
    {
        // Standard singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // Subscribe to sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from sceneLoaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        // Accumulate total time
        totalTime += Time.deltaTime;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If you have a timer on each level and want to accumulate it, you’d need to:
        // 1) Keep a reference to that timer in this script, or
        // 2) Pass the time from that timer before the scene changes.
        // 
        // Example (if you had a LevelTimer script):
        // GameManager.instance.cumulativeTime += levelTimer.elapsedTime;

        // If the newly loaded scene is the MainMenu,
        // you could store finalTime here and/or remove the clockUI.
        if (scene.name == "MainMenu")
        {
            // Store the final time if you want to reset or display it
            finalTime = totalTime;

            if (clockUI != null)
            {
                Destroy(clockUI);
            }
        }
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Current Score = " + currentScore);
    }
}

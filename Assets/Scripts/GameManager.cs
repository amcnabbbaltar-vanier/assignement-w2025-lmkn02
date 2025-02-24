using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int currentScore = 0;
    public float finalTime = 0f;  //  <-- Add this line for storing final time

    public GameObject clockUI;

    private void Awake()
    {
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainMenu")
        {
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

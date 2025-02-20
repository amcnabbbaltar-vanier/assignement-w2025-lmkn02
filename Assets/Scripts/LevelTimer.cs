using UnityEngine;
using UnityEngine.UI;       // or "using TMPro;" if using TextMeshPro
using UnityEngine.SceneManagement;
using TMPro;



public class LevelTimer : MonoBehaviour
{
    
    public TextMeshProUGUI clockText; // If using TextMeshPro, comment out the above line

    private float elapsedTime = 0f;
    private bool isPaused = false;

    private void Start()
    {
        elapsedTime = 0f;
        UpdateClockText();
    }

    private void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateClockText();
        }
    }

    private void UpdateClockText()
    {
        // Format to two decimal places
        clockText.text = "Time: " + elapsedTime.ToString("F2") + "s";
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateClockText();
    }

    public void SetPaused(bool pause)
    {
        isPaused = pause;
    }
}

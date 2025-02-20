using UnityEngine;
using UnityEngine.UI;       
using UnityEngine.SceneManagement;
using TMPro;



public class LevelTimer : MonoBehaviour
{
    
    public TextMeshProUGUI clockText; 

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

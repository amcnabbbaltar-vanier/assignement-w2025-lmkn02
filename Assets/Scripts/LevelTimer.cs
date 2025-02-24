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
        
        UpdateClockText();
    }

    private void Update()
    {
        if (!isPaused)
        {
            elapsedTime += Time.deltaTime;
            UpdateClockText();

           
            if (GameManager.instance != null)
            {
                GameManager.instance.finalTime = elapsedTime;
            }
        }
    }

    private void UpdateClockText()
    {
      
        clockText.text = "Time: " + GameManager.instance.totalTime.ToString("F2") + "s";
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

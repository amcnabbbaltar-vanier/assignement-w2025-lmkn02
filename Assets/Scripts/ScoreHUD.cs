using UnityEngine;
using UnityEngine.UI; 
using TMPro;
public class ScoreHUD : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText; // If using TextMeshPro

    void Update()
    {
        
        int currentScore = GameManager.instance.currentScore;
        scoreText.text = "Score: " + currentScore;
    }
}

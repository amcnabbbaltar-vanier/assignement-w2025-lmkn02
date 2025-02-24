using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameDisplay : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    void Start()
    {
       
        int score = GameManager.instance.currentScore;
        float time = GameManager.instance.finalTime;

        finalScoreText.text = "Final Score: " + GameManager.instance.currentScore;
        finalTimeText.text = "Time: " + GameManager.instance.totalTime;

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

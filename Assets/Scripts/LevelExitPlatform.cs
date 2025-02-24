using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExitPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            int currentIndex = SceneManager.GetActiveScene().buildIndex;

            
            int nextIndex = currentIndex + 1;

            
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
            
                SceneManager.LoadScene(nextIndex);
            }
            else
            {

                SceneManager.LoadScene("EndGameScene");

            }
        }
    }
}

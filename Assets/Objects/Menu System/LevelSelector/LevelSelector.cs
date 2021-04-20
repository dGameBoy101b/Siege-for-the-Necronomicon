using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Select(string levelName)
    {
        //load data and get index for the level to load
        GameObject.FindObjectOfType<Player>().LoadPlayer();
        int i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);

        GameObject.FindObjectOfType<Health>().currentHealth = GameObject.FindObjectOfType<Player>().health[i];
        GameObject.FindObjectOfType<UITimer>().timeRemaining = GameObject.FindObjectOfType<Player>().timeLeft[i];
        GameObject.FindObjectOfType<ScoreSystem>().currentScore = GameObject.FindObjectOfType<Player>().highScore[i];

        SceneManager.LoadScene(levelName);

        Time.timeScale = 1;
    }
    
}

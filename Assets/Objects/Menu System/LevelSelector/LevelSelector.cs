using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Select(string levelName)
    {
        //load data and get index for the level to load
        int i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);
        SceneManager.LoadScene(levelName);

        GameObject.FindObjectOfType<Player>().LoadPlayer();
        GameObject.FindObjectOfType<Health>().currentHealth = GameObject.FindObjectOfType<Player>().health[i];
        GameObject.FindObjectOfType<ScoreSystem>().currentScore = GameObject.FindObjectOfType<Player>().highScore[i];
        GameObject.FindObjectOfType<UITimer>().timeRemaining = GameObject.FindObjectOfType<Player>().timeLeft[i];

    }
    
}

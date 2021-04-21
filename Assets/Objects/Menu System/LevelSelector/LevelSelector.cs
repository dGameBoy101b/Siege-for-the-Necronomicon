using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    
    /**
    *@param levelName is the name of the level that the user wants to load
    */
    public void Select(string levelName)
    {
        //load data and get index for the level to load
        int i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);
        SceneManager.LoadScene(levelName);

        //should set the game variables to the save instances but doesnt work
        GameObject.FindObjectOfType<Player>().LoadPlayer();
        GameObject.FindObjectOfType<Health>().currentHealth = GameObject.FindObjectOfType<Player>().health[i];
        GameObject.FindObjectOfType<ScoreSystem>().currentScore = GameObject.FindObjectOfType<Player>().highScore[i];
        GameObject.FindObjectOfType<UITimer>().timeRemaining = GameObject.FindObjectOfType<Player>().timeLeft[i];

    }
    
}

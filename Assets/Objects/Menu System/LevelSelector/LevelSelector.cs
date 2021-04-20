using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Select(string levelName)
    {
        //load data and get index for the level to load
        PlayerData temp = SaveLoad.LoadData();
        int i = temp.FindIndex(levelName);

        GameObject.FindObjectOfType<Health>().currentHealth = temp.health[i];
        GameObject.FindObjectOfType<UITimer>().timeRemaining = temp.timeLeft[i];
        GameObject.FindObjectOfType<ScoreSystem>().currentScore = temp.highScore[i];

        SceneManager.LoadScene(levelName);

        Time.timeScale = 1;
    }
    
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void Select(string levelName)
    {
        //load data and get index for the level to load
        int i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);

        GameObject.FindObjectOfType<Player>().LoadPlayer();

        SceneManager.LoadScene(levelName);
    }
    
}

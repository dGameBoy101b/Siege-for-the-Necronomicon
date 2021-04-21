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
    }
    
}

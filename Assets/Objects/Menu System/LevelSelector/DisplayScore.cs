using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public string levelName;
    
    // Start is called before the first frame update
    void Start()
    {
        levelName = this.name;
        this.GetComponentInChildren<Text>().text = levelName + "\n" + "High Score: " + GetScore();
    }

  
    private int GetScore()
    {   
        int i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);
        
        return(GameObject.FindObjectOfType<Player>().highScore[i]);
    }
    
}

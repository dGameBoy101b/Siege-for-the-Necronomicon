using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public string levelName;
    public int score;
    public int i;
    
    // Start is called before the first frame update
    void Update()
    {
        levelName = this.name;
        score = GetScore();
        this.GetComponentInChildren<Text>().text = levelName + "\n" + "High Score: " + score;
    }

  
    private int GetScore()
    {   
        i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);
        
        return(GameObject.FindObjectOfType<Player>().highScore[i]);
    }
    
}

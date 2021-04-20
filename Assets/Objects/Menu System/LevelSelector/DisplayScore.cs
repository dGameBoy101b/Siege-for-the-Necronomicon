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
        this.GetComponent<Text>().text = levelName + "\n" + "High Score: " + GetScore();
    }

  
    private int GetScore()
    {   
        PlayerData scores = SaveLoad.LoadData();
        int i = scores.FindIndex(levelName);
        return(scores.highScore[i]);
    }
    
}

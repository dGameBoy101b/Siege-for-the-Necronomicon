using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public string levelName;
    public int score;
    public bool complete;
    public int index;
    public float timeLeft;
    
    void Update()
    {
        levelName = this.name;
        index = GetIndex();
        complete = GetComplete(index);

        if(complete == true)
        {
            score = GetScore(index);
            this.GetComponentInChildren<Text>().text = levelName + "\n" + "High Score: " + score;
        }else
        {
            timeLeft = GetTimeLeft(index);
            this.GetComponentInChildren<Text>().text = levelName + "\n" + "Time Left: " + timeLeft;
        }
        
    }

    private int GetIndex()
    {
        int i = GameObject.FindObjectOfType<Player>().FindIndex(levelName);
        return(i);
    }

    private int GetScore(int i)
    {   
        return(GameObject.FindObjectOfType<Player>().highScore[i]);
    }

    private bool GetComplete(int i)
    {
        return(GameObject.FindObjectOfType<Player>().completed[i]);
    }
    
    private float GetTimeLeft(int i) 
    {
        return(GameObject.FindObjectOfType<Player>().timeLeft[i]);
    }
}

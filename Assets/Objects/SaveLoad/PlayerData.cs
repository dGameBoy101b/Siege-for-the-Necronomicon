using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
*@author Connor Burnside 33394927   
*@date 9/04/2021
*A class that holds player data to seriazlize in the save/load script, changing the data type if necessary
* serializable and not mono because it isnt a class to be used on an object
*/
[System.Serializable]
public class PlayerData 
{

    public int[] health;
    public int[] highScore;
    public bool[] completed; 
    public float[] timeLeft;
    public string[] scenes;

    /**
    * @param player this is the class that is going to be saved 
    * constructor for the playerdata class, param should be changed to tha class that has the data to save 
    * data is saved for each level, arrays index corresponds to the level
    */
    public PlayerData(Player player)
    {
        PlayerData temp = SaveLoad.LoadData();
        
        health = new int[player.size];
        highScore = new int[player.size];
        completed = new bool[player.size];
        timeLeft = new float[player.size];
        
        for(int i = 0; i < player.size; i++)
        {  
            health[i] = player.health[i];
            highScore[i] = player.highScore[i];
            completed[i] = player.completed[i];
            timeLeft[i] = player.timeLeft[i];
        }
    }




    /**
    * a function that returns the index of a specific scene
    
    public int FindIndex(string levelName)
    {
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            if(scenes[i] == levelName)
            {
                return(i);
            }
        }

        Debug.LogError("level doesnt exist");
        return(-1);
    }*/
}

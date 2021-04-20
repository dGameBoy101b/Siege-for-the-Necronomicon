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
    private string[] scenes;

    /**
    * @param player this is the class that is going to be saved 
    * constructor for the playerdata class, param should be changed to tha class that has the data to save 
    * data is saved for each level, arrays index corresponds to the level
    */
    public PlayerData(TestPlayer player)
    {
        ReadScenes();
       
        int size = scenes.Length;

        int index = FindIndex(SceneManager.GetActiveScene().name);

        PlayerData temp = SaveLoad.LoadData();
        
        health = new int[size];
        highScore = new int[size];
        completed = new bool[size];
        timeLeft = new float[size];
        
        for(int i = 0; i < size; i++)
        {  
            //if its current level save new data
            if(i == index)
            {
                health[i] = 0 ;
                completed[i] = false;
                if(completed[i] == true)
                {
                    highScore[i] = 0;
                    timeLeft[i] = 0f;
                }
                else
                {   
                    timeLeft[i] = 0;
                    highScore[i] = 0;
                }
                
            }else //else resave the old data
            {
                health[i] = temp.health[i];
                completed[i] = temp.completed[i];
                highScore[i] = temp.highScore[i];
                timeLeft[i] = temp.timeLeft[i];
            }
        }
    }

    /**
    *this reads the scenes in the game build and stores them as an array so that the save data for each can be found
    */
    private void ReadScenes()
    {
        string[] scenes = new string[SceneManager.sceneCountInBuildSettings];

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenes[i] = SceneManager.GetSceneByBuildIndex(i).name;
        }
    }

    /**
    * a function that returns the index of a specific scene
    */
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
    }
}

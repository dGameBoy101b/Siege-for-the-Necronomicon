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
        int size = scenes.Length;
        
        health = new int[size];
        highScore = new int[size];
        completed = new bool[size];
        timeLeft = new float[size];
        
        for(int i = 0; i < size; i++)
        {  
            /*set variables for each level here
            health[i] = 
            completed[i] = 
            if(completed[i] == true)
            {
                highScore[i] =
            }
            timeLeft[i] = 
            */
        }

    }

    private void ReadScenes()
    {
        string[] scenes = new string[SceneManager.sceneCountInBuildSettings];

        for(int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            scenes[i] = SceneManager.GetSceneByBuildIndex(i).name;
        }
    }
}

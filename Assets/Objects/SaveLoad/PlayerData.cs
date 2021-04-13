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
    public int[] score;
    public bool[] completed; 
    public float[] timeLeft;

    /**
    * @param player this is the class that is going to be saved 
    * constructor for the playerdata class, param should be changed to tha class that has the data to save 
    * data is saved for each level, arrays index corresponds to the level
    */
    public PlayerData(TestPlayer player)
    {
        //size is the amount of levels,the arrays will correspond with levels in order its -1 to not include main menu
        int size = SceneManager.sceneCountInBuildSettings - 1;
        
        health = new int[size];
        score = new int[size];
        completed = new bool[size];
        timeLeft = new float[size];
        
        for(int i = 0; i < size; i++)
        {
            //set variables for each level here
            //health[i] = 
            //score[i] =
            //completed[i] = 
            //timeLeft[i] = 
        }

    }
}

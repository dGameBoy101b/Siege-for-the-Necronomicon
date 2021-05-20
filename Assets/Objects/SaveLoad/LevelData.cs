using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* @author Connor Burnside 33394927   
* @date 9/04/2021
* A class that holds player data to seriazlize in the save/load script, changing the data type if necessary
* serializable and not mono because it isnt a class to be used on an object
*/

[System.Serializable()]
public class LevelData
{
	/**
	 * The path to the linked level.
	 */
	public string Path;
	
	/**
	 * The lowest time remaining in seconds for this level.
	 */
	public float TimeLeft;
	
	/**
	 * The highest recorded score for this level.
	 */
	public int HighScore;
	
	/**
	 * Construct a level data from given data.
	 * @param path The string path to the level associated with the given data.
	 * @param time The float number of seconds remaining until the given level was completed.
	 * @param score The integer number of points scored after finishing the given level.
	 */
	public LevelData(string path, float time, int score)
	{
		this.Path = path;
		this.TimeLeft = time;
		this.HighScore = score;
	}

	/**
	 * Construct a copy of the given level data.
	 * @param data The level data to copy.
	 */
	public LevelData(LevelData data)
    {
		this.Path = data.Path;
		this.TimeLeft = data.TimeLeft;
		this.HighScore = data.HighScore;
    }
	
	/**
	 * Test if the level linked to this data is complete.
	 */
	public bool isComplete()
	{
		return this.TimeLeft <= 0f;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
* @author Connor Burnside 33394927  
* @date 20/04/2021
* The class that should store all variables to save and load
*/

public class Player : MonoBehaviour
{
	public int[] highScore;
	
	public bool[] completed;
	
	public float[] timeLeft;
	
	[Tooltip("The levels to display information of.")]
	public string[] scenes;
	
	public string currentscene;
	
	public int size;
	
	[Tooltip("a static Player variable that is used to enforce singleton property")]
	public static Player instance = null;

	// Start is called before the first frame update
	void Awake()
	{
		//singleton pr
		if(instance == null)
		{
			instance = this;
		}else if (instance != this)
		{
			Destroy(gameObject);
		}
		
		ReadScenes();
		currentscene = SceneManager.GetActiveScene().name;


		highScore = new int[size];
		completed = new bool[size];
		timeLeft = new float[size];
	}
	
	private void Start() 
	{
		//DontDestroyOnLoad(this);
		try{
		LoadPlayer();
		}
		catch (System.Exception) {}
	}


	public void UpdateStats()
	{ 
		int j = 0;
		for(int i = 0; i < size; i++)
		{
			if(scenes[i] == currentscene)
			{
				j = i;
			}
		}

		timeLeft[j] = GameObject.FindObjectOfType<LevelTimer>().TIME;
		if(timeLeft[j] == 0)
		{
			completed[j] = true;
		}
		if( GameObject.FindObjectOfType<ScoreSystem>().currentScore > highScore[j])
		{
			highScore[j] = GameObject.FindObjectOfType<ScoreSystem>().currentScore;
		}
		
	}

	/**
	*this reads the scenes in the game build and stores them as an array so
	* that the save data for each can be found
	*/
	private void ReadScenes()
	{
		size = SceneManager.sceneCountInBuildSettings;
		scenes = new string[size];

		for(int i = 0; i < size; i++)
		{
			string pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
			scenes[i] = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
		}
	}

	/**
	* given the name of a level will get the index of that level in the scenes array, 
	* this can be used to find the relevant data from the other array
	* @param levelName the name of the level that you are trying to find
	*/
	public int FindIndex(string levelName)
	{
		for(int i = 0; i < size; i++)
		{
			if(scenes[i] == levelName)
			{
				return(i);
			}
		}

		Debug.LogError("level doesnt exist");
		return(-1);
	}

	/**
	* gives this instance to the savedata script for serialisation and saving
	*/
	public void SavePlayer()
	{
		SaveLoad.SaveData(this);
	}

	/**
	* calls the saveload load function and sets the variables
	*/
	public void LoadPlayer()
	{
		PlayerData data = SaveLoad.LoadData();
		for(int i = 0; i < size; i++)
		{
			if(data.highScore[i] > highScore[i])
			{
				highScore[i] = data.highScore[i];
			}
			completed[i] = data.completed[i];
			timeLeft[i] = data.timeLeft[i];
		}
	}
}

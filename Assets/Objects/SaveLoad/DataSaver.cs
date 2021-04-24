using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * @author Rhys Mader 33705134
 * @date 24/04/2021
 * A class used to save level data.
 */
public sealed class DataSaver : MonoBehaviour
{
	[Header("Linked Files")]
	
	[SerializeField()]
	[Tooltip("The file to save data to")]
	public string DATA_PATH;
	
	[Header("Level Information")]
	
	[SerializeField()]
	[Tooltip("The timer in this level to save.")]
	public LevelTimer TIMER;
	
	[SerializeField()]
	[Tooltip("The player's score to save.")]
	public ScoreSystem SCORE;
	
	/**
	 * The path to the scene to save.
	 */
	public string ScenePath {get; private set;}
	
	/**
	 * Save the level data linked to this data saver.
	 */
	public void saveData()
	{
		this.fetchCurrentScene();
		SaveLoad.saveData(this.DATA_PATH, new LevelData(this.ScenePath, this.TIMER.TIME, this.SCORE.currentScore));
	}
	
	/**
	 * Fetch the currently active scene and store its path.
	 */
	private void fetchCurrentScene()
	{
		this.ScenePath = SceneManager.GetActiveScene().path;
	}
	
	private void OnEnable()
	{
		this.saveData();
	}
}

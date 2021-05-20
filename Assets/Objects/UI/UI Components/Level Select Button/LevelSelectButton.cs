using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class LevelSelectButton : MonoBehaviour
{
	[Header("Linked Files")]
	
	[SerializeField()]
	[Tooltip("The path of the scene this button loads when clicked.")]
	public string SCENE_PATH;
	
	[SerializeField()]
	[Tooltip("The path to the saved data about the level this button is linked to.")]
	public string DATA_PATH;
	
	[Header("Text Displays")]
	
	[SerializeField()]
	[Tooltip("The text element to display the level name.")]
	public ValueDisplay LEVEL_DISPLAY;
	
	[SerializeField()]
	[Tooltip("The element to enable to show the highscore.")]
	public ValueDisplay SCORE_DISPLAY;
	
	[SerializeField()]
	[Tooltip("The element to enable to show the time remaining.")]
	public ValueDisplay TIME_DISPLAY;
	
	[Header("Level Statistics")]
	
	[SerializeField()]
	[Tooltip("The displayed name of the level.")]
	public string LEVEL_NAME;

	[SerializeField()]
	[Tooltip("Should this level be unlocked?")]
	public bool UNLOCKED;

	[SerializeField()]
	[Tooltip("The level to unlock when this one is completed.")]
	public LevelSelectButton DEPENDENT;
	
	/**
	 * The data about the linked level.
	 */
	public LevelData Data {get; private set;}
	
	/**
	 * Whether the linked level has been found.
	 */
	public bool ValidScene
	{
		get
		{
			return this.BuildIndex > -1;
		}
	}
	
	/**
	 * The build index of the scene this button is linked to via the path given to this button.
	 */
	public int BuildIndex {get; private set;} = -1;
	
	/**
	 * The button this script is attached to.
	 */
	private Button button;
	
	/**
	 * Update all the linked text to display the fetched data.
	 */
	public void updateText()
	{
		this.fetchScene();
		this.fetchData();
		this.LEVEL_DISPLAY.Value = this.LEVEL_NAME;
		this.LEVEL_DISPLAY.updateText();
		if (this.Data != null)
		{
			this.SCORE_DISPLAY.Value = this.Data.HighScore;
			this.SCORE_DISPLAY.updateText();
			this.TIME_DISPLAY.Value = this.Data.TimeLeft;
			this.TIME_DISPLAY.updateText();
			if (this.DEPENDENT != null)
			{
				this.DEPENDENT.UNLOCKED = this.Data.isComplete();
			}
		}
		this.TIME_DISPLAY.gameObject.SetActive(this.Data != null && !this.Data.isComplete());
		this.SCORE_DISPLAY.gameObject.SetActive(this.Data != null && this.Data.isComplete());
		this.button.interactable = this.ValidScene && this.UNLOCKED;
	}
	
	/**
	 * Load the linked scene.
	 * @throws Exception The path given to this button is not a valid scene.
	 */
	public void loadLevel()
	{
		this.fetchScene();
		if (!this.ValidScene)
		{
			throw new System.Exception("No valid scene found.");
		}
		SceneManager.LoadSceneAsync(this.BuildIndex, LoadSceneMode.Single);
	}
	
	/**
	 * Fetch the scene this button is linked to.
	 */
	private void fetchScene()
	{
		this.BuildIndex = SceneUtility.GetBuildIndexByScenePath(this.SCENE_PATH);
	}
	
	/**
	 * Fetch the level data this button is linked to.
	 */
	private void fetchData()
	{
		LevelData data = SaveLoad.loadData(this.DATA_PATH);
		if (data == null)
		{
			this.Data = null;
		}
		else
		{
			this.Data = new LevelData(data);
		}
		if (this.Data != null && this.Data.Path != this.SCENE_PATH)
		{
			Debug.LogWarning("Mismatched level path in level data (\"" + this.Data.Path + "\") and linked scene (\"" + this.SCENE_PATH + "\").");
		}
	}
	
	private void OnEnable()
	{
		this.updateText();
	}
	
	private void Awake()
	{
		this.button = this.GetComponent<Button>();
		this.fetchScene();
		this.fetchData();
	}
}

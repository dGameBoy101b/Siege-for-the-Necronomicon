using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class LevelSelectButton : MonoBehaviour
{
	[Tooltip("The path of the scene this button loads when clicked.")]
	public string LOAD_PATH;
	
	[Header("Text Displays")]
	
	[Tooltip("The text element to display the level name.")]
	public ValueDisplay LEVEL_DISPLAY;
	
	[Tooltip("The element to enable to show the highscore.")]
	public ValueDisplay SCORE_DISPLAY;
	
	[Tooltip("The element to enable to show the time remaining.")]
	public ValueDisplay TIME_DISPLAY;
	
	[Header("Level Statistics")]
	
	[Tooltip("The displayed name of the level.")]
	public string LEVEL_NAME;
	
	[Tooltip("The displayed highscore of the level.")]
	public int HIGH_SCORE;
	
	[Tooltip("The displayed time in seconds until the level was completed.")]
	public float TIME_LEFT;
	
	/**
	 * Whether the linked level is complete.
	 */
	public bool IsComplete
	{
		get
		{
			return this.TIME_LEFT <= 0f;
		}
	}
	
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
	 * Update all the linked text to display the set attributes.
	 */
	public void updateText()
	{
		this.fetchScene();
		this.LEVEL_DISPLAY.Value = this.LEVEL_NAME;
		this.LEVEL_DISPLAY.updateText();
		this.SCORE_DISPLAY.Value = this.HIGH_SCORE;
		this.SCORE_DISPLAY.updateText();
		this.TIME_DISPLAY.Value = this.TIME_LEFT;
		this.TIME_DISPLAY.updateText();
		this.TIME_DISPLAY.gameObject.SetActive(!this.IsComplete);
		this.SCORE_DISPLAY.gameObject.SetActive(this.IsComplete);
		this.button.interactable = this.ValidScene;
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
		this.BuildIndex = SceneUtility.GetBuildIndexByScenePath(this.LOAD_PATH);
	}
	
	private void OnEnable()
	{
		this.updateText();
	}
	
	private void Awake()
	{
		this.button = this.GetComponent<Button>();
		this.fetchScene();
	}
}

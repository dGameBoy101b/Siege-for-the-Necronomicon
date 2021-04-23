using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class LevelSelectButton : MonoBehaviour
{
	[Tooltip("The build index of the scene this button loads when clicked.")]
	public int LOAD_SCENE;
	
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
	 * Update all the linked text to display the set attributes.
	 */
	public void UpdateText()
	{
		this.LEVEL_DISPLAY.Value = this.LEVEL_NAME;
		this.LEVEL_DISPLAY.updateText();
		this.SCORE_DISPLAY.Value = this.HIGH_SCORE;
		this.SCORE_DISPLAY.updateText();
		this.TIME_DISPLAY.Value = this.TIME_LEFT;
		this.TIME_DISPLAY.updateText();
		this.TIME_DISPLAY.gameObject.SetActive(!this.IsComplete);
		this.SCORE_DISPLAY.gameObject.SetActive(this.IsComplete);
	}
	
	/**
	 * Load the linked scene.
	 */
	public void loadLevel()
	{
		SceneManager.LoadSceneAsync(this.LOAD_SCENE, LoadSceneMode.Single);
	}
	
	private void OnEnable()
	{
		this.UpdateText();
	}
}

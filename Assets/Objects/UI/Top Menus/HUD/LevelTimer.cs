using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author Kai Sweeting 33268787
 * @date 14/04/2021
 * A count down timer for the UI
 */
public class LevelTimer : MonoBehaviour
{
	[Tooltip("The object that spawns waves of projectiles.")]
	public WaveManager WAVE_MANAGER;

	[Tooltip("The amount of time left on the timer (seconds).")]
	public float TIME;

	[Tooltip("The text object the timer is displayed on.")]
	public TMPro.TMP_Text TIME_TEXT;
	
	[Tooltip("The canvas to show when the level is completed.")]
	public Canvas LEVEL_COMPLETE;
	
	[Tooltip("the canvas to hide when the level is completed.")]
	public Canvas HUD;

	public Transition TRANSITION;

	[HideInInspector]
	public bool timerIsRunning;

	private void Start()
	{
		timerIsRunning = false;
	}
	
	private void Update()
	{
		this.updateTimer();
	}

	/**
	 * Check if timer is enabled and running
	 * If timer hits zero display game over and disable wave manager spawning projectiles
	 * @param timerIsRunning The bool that checks if timer is running or not
	 * @param timeRemaining The float that shows in minutes and seconds the timer left on the timer
	 * @return timeRemaining To DisplayTimer to show countdown on UI
	 */
	void updateTimer()
	{
		if (timerIsRunning)
		{
			if (TIME > 0)
			{
				TIME -= Time.deltaTime;
				DisplayTimer(TIME);
			}
			else
			{
				TimerOver();
			}
		}
	}

	/** 
	 * Calculate the time remaining from specified seconds
	 * Display remaining minutes and seconds values in text
	 * @param showTime The float timeRemaining for seconds specified
	*/
	void DisplayTimer(float showTime)
	{
		showTime += 1;
		float minutes = Mathf.FloorToInt(showTime / 60); 
		float seconds = Mathf.FloorToInt(showTime % 60);
		this.TIME_TEXT.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	/** 
	 * Stop wave manager from spawning more projectiles and show game over screen
	*/
	void TimerOver()
	{
		this.timerIsRunning = false;
		this.WAVE_MANAGER.gameObject.SetActive(false);
		this.TRANSITION.levelCompleted();
		this.HUD.enabled = false;
		FindObjectOfType<LaserPointer>().GetComponent<LineRenderer>().enabled = true;
		Cursor.lockState = CursorLockMode.Confined;
	}
}
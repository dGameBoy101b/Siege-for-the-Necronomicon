using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author Kai Sweeting 33268787
 * @date 14/04/2021
 * A count down timer for the UI
 */
public class UITimer : MonoBehaviour
{
	public WaveManager WAVE_MANAGER;
	public float timeRemaining;
	public Text timeText;
	public Image gameOver;

	[HideInInspector]
	public bool timerIsRunning;

	[HideInInspector]
	public GameObject player;

	private GameObject projectile;

	private void Start()
	{
		timerIsRunning = true;
	}

	/**
	 * Check if timer is enabled and running
	 * If timer hits zero display game over and disable wave manager spawning projectiles
	 * @param timerIsRunning The bool that checks if timer is running or not
	 * @param timeRemaining The float that shows in minutes and seconds the timer left on the timer
	 * @return timeRemaining To DisplayTimer to show countdown on UI
	 */
	void Update()
	{
		//null ref on time up
		 gameOver.gameObject.SetActive(!timerIsRunning);
		if (timerIsRunning)
		{
			if (timeRemaining > 0)
			{
				timeRemaining -= Time.deltaTime;
				DisplayTimer(timeRemaining);
			}
			else
			{
				timeRemaining = 0;
				timerIsRunning = false;
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
		timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}

	/** 
	 * if the timer has stopped, stop wave manager from spawning more projectiles and show game over screen
	*/
	void TimerOver()
	{   
		if(!timerIsRunning)
		{
			this.WAVE_MANAGER.enabled = false;
			Update();
		}
	}
}
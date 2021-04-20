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
    public float timeRemaining;
    public Text timeText;
    public Image gameOver;
    Health health;

    [HideInInspector]
    public bool timerIsRunning;

    [HideInInspector]
    public GameObject player;


    private GameObject projectile;


    private void Start()
    {

        timerIsRunning = true;
        projectile = GameObject.FindWithTag("ProjectileSpawn");
        player = GameObject.FindWithTag("Player");
		health = player.GetComponent<Health>();
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
         gameOver.gameObject.SetActive(!timerIsRunning && health.currentHealth > 0);

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
     *@param showTime The float timeRemaining for seconds specified
     */
    void DisplayTimer(float showTime)
    {

        showTime += 1;

        /**
         * calculate the time remaining from specified seconds
         */
        float minutes = Mathf.FloorToInt(showTime / 60); 
        float seconds = Mathf.FloorToInt(showTime % 60);
        
        /**
         * display remaining minutes and seconds values in text
         */
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerOver()
    {   /** if the timer has stopped, stop wave manager from spawning more projectiles and show game over screen
        */
        if(!timerIsRunning)
        {
            projectile.GetComponent<WaveManager>().enabled = false;
            Update();
        }

    }
}
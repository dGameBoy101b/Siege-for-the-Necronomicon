using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;

    private void Start()
    {
        /**
         * Set timer to true on application start
         * automatically starts if true
         */

        timerIsRunning = true;
    }

    void Update()
    {
        /**
         * Check if timer is set to true
         * only execute if-statement if timer is set to true
         */

        if (timerIsRunning)
        {
            /** 
             * If true and greater than 0 start counting down from specified seconds
             */
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                /**
                 * @return timeRemaining To DisplayTimer to show countdown on UI
                 */
                DisplayTimer(timeRemaining);
            }
            else
            {
                /**
                 * If timer hits zero display game over
                 * set timerIsRunning to false and lock timer 
                 */
                
                Debug.Log("Game Over!");
                timeRemaining = 0;
                timerIsRunning = false;
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
}
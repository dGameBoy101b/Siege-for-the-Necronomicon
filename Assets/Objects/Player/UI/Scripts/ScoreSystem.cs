﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author Kai Sweeting 33268787
 * @date 14/04/2021
 * Score system that increases on projectile collision with the player's sword
 */

public class ScoreSystem : MonoBehaviour
{


    public Text currentScoreLabel;
    public Text endScoreLabel;
    public Text endTimeScoreLabel;

    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public int endScore;
    


    void Start()
    {

        currentScore = 0;
        endScore = currentScore;
        ScoreUpdate();
        
    }

    /**
     * set the public text variables to store the current player score in a string
     * this is so it can be displayed on game over on during gameplay
     */

    void ScoreUpdate()
    {
        currentScoreLabel.text = currentScore.ToString();
        endScoreLabel.text = currentScore.ToString();
        endTimeScoreLabel.text = currentScore.ToString();
    }

    /**
     * @param col The collider projectile that is hitting the player sword and adding score points
     * score increases by 10 on every successful hit of projectile
     * destroys the projectile on hit
     */

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            currentScore += 10;
            ScoreUpdate();
            Destroy(col.gameObject);

        }

    }

}

using System.Collections;
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

	[Tooltip("The text object that stores the player's current score.")]
	public Text currentScoreLabel;

	[Tooltip("The text object that stores the player's final score to display on game over.")]
	public ValueDisplay endScoreLabel;

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
	 * this is so it can be displayed on game over or during gameplay
	 */

	void ScoreUpdate()
	{
		currentScoreLabel.text = currentScore.ToString();
		endScoreLabel.Value = currentScore;
		this.endScoreLabel.updateText();
	}

	/**
	 * @param col The collider projectile that is hitting the player sword and adding score points
	 * score increases by 10 on every successful hit of projectile
	 * destroys the projectile on hit
	 */

	public void AddScore(int points)
	{
		currentScore += points;
		ScoreUpdate();
	}

}

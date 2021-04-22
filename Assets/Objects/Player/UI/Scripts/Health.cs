using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author Kai Sweeting 33268787
 * @date 16/04/2021
 * Player health component that displays on the UI and reacts to projectile collisions
 */

public class Health : MonoBehaviour
{
	[Tooltip("The prefab that spawns waves of projectiles.")]
	public WaveManager WAVE_MANAGER;

	[Tooltip("The maximum amount of health the player has.")]
	public int maxHealth;

	[Tooltip("The text object that stores the health of the player.")]
	public Text currentHealthLabel;

	[Tooltip("The image that displays at the game over screen.")]
	public Image gameOver;

	[Tooltip("The player's emergency shield")]
	public EmergencyShield EMERGENCY_SHIELD;

	[HideInInspector]
	public int currentHealth;

	private bool isDead;

	/**
	 * Initialize max health to current
	 * set bool isDead to false so game doesn't end instantly
	 * embed Wave Manager into projectile object to be able disable the waves of attacks on player death or timer reaches 0
	 */
	void Start()
	{
		currentHealth = maxHealth;
		isDead = false;
		UpdateGUI();
	}

	/** 
	 * set the amount of current HP into text on the UI
	 * check if isDead is true yet for enabling game over screen
	*/
	public void UpdateGUI()
	{
		currentHealthLabel.text = currentHealth.ToString();
		gameOver.gameObject.SetActive(isDead);
	}

	/** 
	 * @param damage the amount of damage the player recieves on projectile collision
	 */
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		PlayerDead();
		UpdateGUI();
		EMERGENCY_SHIELD.ActivateInvincibility();
	}
	
	/** 
	 * set isDead to true if 3 damage points have been caused to the player
	 * with isDead true the game over screen will show with player total final score
	 * disable the wave manager from spawning any more projectiles since the game is now over
	 * @return ignore PlayerDead() method and return to previous method if current health does not equal 0 
	 */
	void PlayerDead()
	{
		if(currentHealth == 0)
			{
				isDead = true;
				this.WAVE_MANAGER.enabled = false;
				UpdateGUI();
			}
		else
			return; 
	}
}

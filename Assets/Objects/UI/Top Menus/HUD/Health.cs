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
	public TMPro.TMP_Text currentHealthLabel;

	[Tooltip("The canvas that displays at the game over screen.")]
	public Canvas LEVEL_FAIL;
	
	[Tooltip("The canvas to hide when the player dies.")]
	public Canvas HUD;
	
	[Tooltip("The timer to stop when the player dies.")]
	public LevelTimer TIMER;
	
	[Tooltip("The text that displays the remaining time on the fail menu.")]
	public ValueDisplay TIME_DISPLAY;

	[Tooltip("The player's emergency shield")]
	public EmergencyShield EMERGENCY_SHIELD;

	[Tooltip("GUI component for player's health")]
	public Slider slider;

	[HideInInspector]
	public int currentHealth;

	public bool IsDead
	{
		get
		{
			return this.currentHealth < 1;
		}
	}

	/**
	 * Initialize max health to current
	 * set bool isDead to false so game doesn't end instantly
	 * embed Wave Manager into projectile object to be able disable the waves of attacks on player death or timer reaches 0
	 */
	void Start()
	{
		currentHealth = maxHealth;
		UpdateGUI();
	}

	/** 
	 * set the amount of current HP into text on the UI
	 * check if isDead is true yet for enabling game over screen
	 * update GUI slider to reflect players current health
	*/
	public void UpdateGUI()
	{
		currentHealthLabel.text = currentHealth.ToString();
		LEVEL_FAIL.gameObject.SetActive(IsDead);
		slider.value = currentHealth;
	}

	/** 
	 * @param damage the amount of damage the player recieves on projectile collision
	 */
	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		UpdateGUI();
		if (this.IsDead)
		{
			PlayerDead();
		}
		else
		{
			EMERGENCY_SHIELD.ActivateInvincibility();
		}
	}
	
	/** 
	 * set isDead to true if 3 damage points have been caused to the player
	 * with isDead true the game over screen will show with player total final score
	 * disable the wave manager from spawning any more projectiles since the game is now over
	 * @return ignore PlayerDead() method and return to previous method if current health does not equal 0 
	 */
	void PlayerDead()
	{
		this.TIMER.timerIsRunning = false;
		this.TIME_DISPLAY.Value = this.TIMER.TIME;
		Cursor.lockState = CursorLockMode.Confined;
		this.TIME_DISPLAY.updateText();
		this.WAVE_MANAGER.gameObject.SetActive(false);
		this.LEVEL_FAIL.gameObject.SetActive(true);
		this.HUD.gameObject.SetActive(false);
	}
}

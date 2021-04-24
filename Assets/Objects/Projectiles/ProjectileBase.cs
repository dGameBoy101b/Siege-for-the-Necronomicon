using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * The abstract base class for all projectiles that attack the player.
 */
public abstract class ProjectileBase : MonoBehaviour
{
	/**
	 * The names of the collision layers that this projectile should attack.
	 */
	public string[] ATTACK_LAYERS {get;} = {"Player"};
	
	/**
	 * The names of the collision layers that this projectile should be destroyed by.
	 */
	public string[] DESTROY_LAYERS {get;} = {"Default"};
	
	/**
	 * The names of the collision layers that this projectile should be defeated by.
	 */
	public abstract string[] DEFEAT_LAYERS {get;}
	
	[Header("Player Information")]
	
	[SerializeField()]
	[Tooltip("The player's health.")]
	public Health PLAYER_HEALTH;
	
	[SerializeField()]
	[Tooltip("The player's score.")]
	public ScoreSystem PLAYER_SCORE;
	
	[Header("Sound Effects")]
	
	[SerializeField()]
	[Tooltip("The sounds played when this projectile is spawned.")]
	public AudioSource[] SPAWN_SOUNDS;
	
	[SerializeField()]
	[Tooltip("The sounds played when this projectile attacks.")]
	public AudioSource[] ATTACK_SOUNDS;
	
	[SerializeField()]
	[Tooltip("The sounds played when this projectile is defeated.")]
	public AudioSource[] DEFEAT_SOUNDS;
	
	[SerializeField()]
	[Tooltip("The sounds played when this projectile is destroyed.")]
	public AudioSource[] DESTROY_SOUNDS;
	
	/**
	 * Test if the given collider is part of a collision layer this projectile should be destroyed by.
	 * @param col The collider to test.
	 * @return True if the given collider should destroy this projectile, false otherwise.
	 */
	public bool shouldBeDestroyed(Collider col)
	{
		return (LayerMask.GetMask(this.DESTROY_LAYERS) 
		        & LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Test if the given collider is part of a collision layer projectiles should attack.
	 * @param col The collider to test.
	 * @return True if the given collider should destroy projectiles, false otherwise.
	 */
	public bool shouldAttack(Collider col)
	{
		return (LayerMask.GetMask(this.ATTACK_LAYERS) 
		        & LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Test if the given collider is part of a collision layer this projectile should be defeated by.
	 * @param col The collider to test.
	 * @return True if the given collider should defeat this projectile, false otherwise.
	 */
	public bool shouldBeDefeated(Collider col)
	{
		return (LayerMask.GetMask(this.DEFEAT_LAYERS) 
		        & LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Play all the sounds associated with spawning this projectile.
	 */
	protected void playSpawnSounds()
	{
		foreach (AudioSource aud in this.SPAWN_SOUNDS)
		{
			aud.Play();
		}
	}
	
	/**
	 * Play all the sounds associated with this projectile attacking.
	 */
	protected void playAttackSounds()
	{
		foreach (AudioSource aud in this.ATTACK_SOUNDS)
		{
			aud.Play();
		}
	}
	
	/**
	 * Play all the sounds associated with defeating this projectile.
	 */
	protected void playDefeatSounds()
	{
		foreach (AudioSource aud in this.DEFEAT_SOUNDS)
		{
			aud.Play();
		}
	}
	
	/**
	 * Play all the sounds associated with destroying this projectile.
	 */
	protected void playDestorySounds()
	{
		foreach (AudioSource aud in this.DESTROY_SOUNDS)
		{
			aud.Play();
		}
	}
	
	/**
	 * The function that should be executed when this projectile is spawned.
	 */
	public virtual void spawn()
	{
		this.gameObject.SetActive(true);
		this.playSpawnSounds();
	}
	
	/**
	 * The function executed when this projectile hits the player.
	 */
	public virtual void attack()
	{
		this.playAttackSounds();
		Object.Destroy(this.gameObject);
	}
	
	/**
	 * The function executed when this projectile is successfully defeated by the player.
	 */
	public virtual void defeat()
	{
		this.playDefeatSounds();
		Object.Destroy(this.gameObject);
	}
	
	/**
	 * The function executed when this projectile is destroyed by the terrain or the emergency shield.
	 */
	public virtual void destroy()
	{
		this.playDestorySounds();
		Object.Destroy(this.gameObject);
	}
	
	/**
	 * Return all the audio sources associated with this projectile.
	 */
	private AudioSource[] allSounds()
	{
		List<AudioSource> auds = new List<AudioSource>();
		auds.AddRange(this.SPAWN_SOUNDS);
		auds.AddRange(this.ATTACK_SOUNDS);
		auds.AddRange(this.DESTROY_SOUNDS);
		auds.AddRange(this.DEFEAT_SOUNDS);
		return auds.ToArray();
	}
	
	/**
	 * Pause this projectile.
	 */
	public virtual void pause()
	{
		foreach (AudioSource aud in this.allSounds())
		{
			if (aud.isPlaying)
			{
				aud.Pause();
			}
		}
	}
	
	/**
	 * Unpause this projectile.
	 */
	public virtual void unpause()
	{
		foreach (AudioSource aud in this.allSounds())
		{
			aud.UnPause();
		}
	}
	
	protected virtual void OnTriggerEnter(Collider col)
	{
		if (this.shouldAttack(col))
		{
			this.attack();
		}
		if (this.shouldBeDefeated(col))
		{
			this.defeat();
		}
		if (this.shouldBeDestroyed(col))
		{
			this.destroy();
		}
	}
}

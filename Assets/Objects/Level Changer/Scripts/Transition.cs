using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 12/05/2021
 * The transition manager that handles all the events that will occur throughout a level.
 */

public class Transition : MonoBehaviour
{
	[Header("Starting Components.")]

	[SerializeField()]
	[Tooltip("The wave manager")]
	public GameObject WAVE_MANAGER;

	[SerializeField()]
	[Tooltip("The effect for trapping the player")]
	public ParticleSystem TRAPPED_ANIMATION;

	[SerializeField()]
	[Tooltip("The length of the trapped animation")]
	public float ANIMATION_LENGTH;

	[SerializeField()]
	[Tooltip("Delay before attacks from the wave manager begin")]
	public float START_TIME;

	[SerializeField()]
	[Tooltip("The Starting Orb that the player interacts with to begin in VR")]
	public GameObject STARTING_ORB;

	[SerializeField()]
	[Tooltip("The level timer")]
	public LevelTimer LEVEL_TIMER;

	[Header("Environment Components.")]

	[SerializeField()]
	[Tooltip("Time of Day")]
	public int TOD;

	[SerializeField()]
	[Tooltip("The index of the Envirosky manager presents")]
	public int ENVIRO_PRESET_INDEX;

	[Header("Teleportation Components.")]

	[SerializeField()]
	[Tooltip("The object that that teleport to.")]
	public Transform TELEPORT_TARGET;

	[SerializeField()]
	[Tooltip("The origin teleport effect.")]
	public ParticleSystem START_TELEPORT_ANIMATION;

	[SerializeField()]
	[Tooltip("The destination teleport effect")]
	public ParticleSystem END_TELEPORT_ANIMATION;

	[Header("Player Component.")]

	[SerializeField()]
	[Tooltip("The player avatar")]
	public GameObject PLAYER;

	[Header("Audio Components.")]

	[SerializeField()]
	[Tooltip("All the audio for when the trap spawns")]
	public AudioSource[] TRAP_SPAWN_SOUNDS;

	[SerializeField()]
	[Tooltip("All the audio for the neutral soundtrack")]
	public AudioSource[] NEUTRAL_THEME;

	[SerializeField()]
	[Tooltip("All the audio for the combat soundtrack")]
	public AudioSource[] COMBAT_THEME;

	[SerializeField()]
	[Tooltip("All the sound effects for when the players survives")]
	public AudioSource[] SURVIVED_THEME;

	[SerializeField()]
	[Tooltip("All the sound effects for when the teleporter effects spawn")]
	public AudioSource[] TELEPORT_SPAWN_SOUNDS;

	[SerializeField()]
	[Tooltip("All the sound effects for when the player is teleported")]
	public AudioSource[] TELEPORT_SOUNDS;

	[SerializeField()]
	[Tooltip("The object that will shake the player's camera")]
	public GameObject EARTHQUAKE_SHAKE;

	[SerializeField()]
	[Tooltip("All the sounds for when the camera shakes / the wave manager begins")]
	public AudioSource[] EARTHQUAKE_SOUNDS;

	/*
	 * Disables Wave manager and sets TOD
	 */
	void Start()
	{
		EnviroSkyMgr.instance.SetTimeOfDay(TOD);
		WAVE_MANAGER.SetActive(false);
	}

	 /*
	 * Uses G as a PC testing tool.
	 */
	void Update()
	{
		if (Input.GetAxis("PC Gauntlet") > 0)
		{
			Destroy(STARTING_ORB);
			this.begin();
		}
	}

	 /*
	 * Manages all the functions that help begin the combat phase.
	 */
	public void begin()
	{
		LEVEL_TIMER.timerIsRunning = true;
		EARTHQUAKE_SHAKE.SetActive(true);
		playEarthquakeSounds();
		stopNeutralTheme();
		playCombatTheme();
		EnviroSkyMgr.instance.ChangeWeather(ENVIRO_PRESET_INDEX);
		TRAPPED_ANIMATION.Play();
		playSpawnSounds();
		Invoke("StopTrapAnimation", ANIMATION_LENGTH);
		Invoke("StartAttacks", START_TIME);
	}

	/*
	 * Manages the functions that occur when the player survives successfully.
	 */
	public void levelCompleted()
	{
		stopCombatTheme();
		playSurvivedTheme();
		Invoke("playTeleportationAnimation", 7);
	}

	/*
	 * Plays the teleportation animations.
	 */
	private void playTeleportationAnimation()
	{
		playTeleportSpawnSound();
		START_TELEPORT_ANIMATION.Play();
		END_TELEPORT_ANIMATION.Play();
		Invoke("stopTeleportationAnimation", 3);
		Invoke("teleportPlayer", 3);
	}

	/*
	 * Stops the teleportation animations.
	 */
	private void stopTeleportationAnimation()
	{
		START_TELEPORT_ANIMATION.Stop();
		END_TELEPORT_ANIMATION.Stop();
	}

	/*
	 * Teleports the player to the TELEPORT_TARGET transform.
	 */
	private void teleportPlayer()
	{
		playTeleportSound();
		PLAYER.transform.position = TELEPORT_TARGET.transform.position;
	}

	/*
	 * Enables the Wave Manager.
	 */
	public void StartAttacks()
	{
		WAVE_MANAGER.SetActive(true);
	}

	/*
	 * Stops the trapped animation.
	 */
	public void StopTrapAnimation()
	{
		TRAPPED_ANIMATION.Stop();
	}

	/*
	 * Plays all the sounds for the trap.
	 */
	protected void playSpawnSounds()
	{
		foreach (AudioSource aud in this.TRAP_SPAWN_SOUNDS)
		{
			aud.Play();
		}
	}

	/*
	 * Plays all the sounds for the Neutral soundtrack.
	 */
	protected void playNeutralTheme()
	{
		foreach (AudioSource aud in this.NEUTRAL_THEME)
		{
			aud.Play();
		}
	}

	/*
	 * Stops all the sounds for the Neutral soundtrack.
	 */
	protected void stopNeutralTheme()
	{
		foreach (AudioSource aud in this.NEUTRAL_THEME)
		{
			aud.Stop();
		}
	}

	/*
	 * Plays all the sounds for the combat soundtrack.
	 */
	protected void playCombatTheme()
	{
		foreach (AudioSource aud in this.COMBAT_THEME)
		{
			aud.Play();
		}
	}

	/*
	 * Stops all the sounds for the combat soundtrack.
	 */
	protected void stopCombatTheme()
	{
		foreach (AudioSource aud in this.COMBAT_THEME)
		{
			aud.Stop();
		}
	}

	/*
	 * Plays all the sounds for when the player survives.
	 */
	protected void playSurvivedTheme()
	{
		foreach (AudioSource aud in this.SURVIVED_THEME)
		{
			aud.Play();
		}
	}

	/*
	 * Plays all the sounds for when the teleportation spells spawn.
	 */
	protected void playTeleportSpawnSound()
	{
		foreach (AudioSource aud in this.TELEPORT_SPAWN_SOUNDS)
		{
			aud.Play();
		}
	}

	/*
	 * Plays all the sounds for the when the player teleports.
	 */
	protected void playTeleportSound()
	{
		foreach (AudioSource aud in this.TELEPORT_SOUNDS)
		{
			aud.Play();
		}
	}

	/*
	 * Plays all the sounds for when the combat phase begins.
	 */
	protected void playEarthquakeSounds()
	{
		foreach (AudioSource aud in this.EARTHQUAKE_SOUNDS)
		{
			aud.Play();
		}
	}

}

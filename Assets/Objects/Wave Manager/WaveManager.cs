using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * @author Rhys Mader 33705134
 * @date 13/04/2021
 * A class to randomly spawn waves of attacks.
 */
public sealed class WaveManager : MonoBehaviour
{
	[Header("Player Information")]
	
	[SerializeField()]
	[Tooltip("The player object to target attacks on.")]
	public GameObject PLAYER;
	
	[SerializeField()]
	[Tooltip("The player's health that projectiles damage.")]
	public Health PLAYER_HEALTH;
	
	[SerializeField()]
	[Tooltip("The player's score that projectiles add to when defeated.")]
	public ScoreSystem PLAYER_SCORE;
	
	[Header("Timing")]
	
	[SerializeField()]
	[Tooltip("The minimum number of seconds between attacks.\n(Must be lesser than or equal to the maximum delay.)")]
	[Min(0)]
	public float MIN_DELAY;
	
	[SerializeField()]
	[Tooltip("The maximum number of seconds between attacks.\n(Must be greater than or equal to the minimum delay.)")]
	[Min(0)]
	public float MAX_DELAY;
	
	[SerializeField()]
	[Tooltip("The maximum number of active attacks allowed at any one time.\n(Can be 0 for any number of attacks.)")]
	[Min(0)]
	public int MAX_ACTIVE;
	
	[Header("Attacks")]
	
	[SerializeField()]
	[Tooltip("The pool of attacks this wave manager can randomly pull from.\n(Must be the same length as attack weights.)")]
	public List<AttackBase> ATTACK_POOL;
	
	[SerializeField()]
	[Tooltip("The likelihood of each corresponding attack being randomly chosen.\n(Must be the same length as attack pool.)")]
	public List<int> ATTACK_WEIGHTS;
	
	/**
	 * The sum of all the attack weights.
	 */
	private int total_weight;
	
	/**
	 * The time at which the next wave can be spawned.
	 */
	private float next_spawn;
	
	/**
	 * The list of currently active attacks.
	 */
	private List<List<ProjectileBase>> active_attacks;
	
	/**
	 * Test if another wave can currently be spawned.
	 */
	public bool canSpawnWave()
	{
		return Time.time > this.next_spawn
			&& (this.MAX_ACTIVE == 0
				|| this.active_attacks.Count < this.MAX_ACTIVE);
	}
	
	/**
	 * Reset the spawn timer to spawn an attack some time after now.
	 */
	private void resetTimer()
	{
		this.next_spawn = Time.time + (float)new System.Random().NextDouble() * (this.MAX_DELAY - this.MIN_DELAY) + this.MIN_DELAY;
	}
	
	/**
	 * Spawn a wave.
	 */
	private void spawnWave()
	{
		System.Random rand = new System.Random();
		this.resetTimer();
		this.active_attacks.Add(this.randomAttack());
	}
	
	/**
	 * Spawn a random attack and return the list of projectiles that make up that attack.
	 * @return The list of projectiles spawned in the randomly selected attack.
	 */
	private List<ProjectileBase> randomAttack()
	{
		int r = new System.Random().Next(this.total_weight);
		int weight = 0;
		for (int i = 0; i < this.ATTACK_WEIGHTS.Count; i++)
		{
			weight += this.ATTACK_WEIGHTS[i];
			if (r < weight)
			{
				return this.ATTACK_POOL[i].spawn(this.PLAYER.transform.position, this.PLAYER.transform.rotation, this.PLAYER_HEALTH, this.PLAYER_SCORE);
			}
		}
		throw new Exception("No attack spawned.");
	}
	
	/**
	 * Check that the attack pool and attack weights lists are the same length.
	 * @throws Exception The attack pool and attack weights lists are not the same length.
	 */
	private void checkAttacks()
	{
		if (this.ATTACK_POOL.Count != this.ATTACK_WEIGHTS.Count)
		{
			throw new Exception("The attack pool and attack weights must be the same length.");
		}
	}
	
	/**
	 * Check that the minimum delay is lesser than or equal to the the maximum delay.
	 * @throws Exception The minimum delay is greater than the maximum delay.
	 */
	private void checkDelay()
	{
		if (this.MIN_DELAY > this.MAX_DELAY)
		{
			throw new Exception("The minimum delay must be lesser than or equal to the maximum delay.");
		}
	}
	
	/**
	 * Calculate the sum of the attack weights.
	 */
	private void calcTotalWeight()
	{
		this.total_weight = 0;
		foreach (int ele in this.ATTACK_WEIGHTS)
		{
			this.total_weight += ele;
		}
	}
	
	/**
	 * Trim the currently active attacks to remove destroyed or inactive projectiles and attacks.
	 */
	private void updateActiveAttacks()
	{
		for (int i = this.active_attacks.Count - 1; i > -1; i--)
		{
			for (int j = this.active_attacks[i].Count - 1; j > -1; j--)
			{
				if (this.active_attacks[i][j] == null || !this.active_attacks[i][j].gameObject.activeInHierarchy)
				{
					this.active_attacks[i].RemoveAt(j);
				}
			}
			if (this.active_attacks[i].Count < 1)
			{
				this.active_attacks.RemoveAt(i);
			}
		}
	}
	
	/**
	 * Test if the spawn timer should be reset.
	 * @param prev_attacks The integer number of active attacks there were before trimming the inactive attacks.
	 */
	private bool shouldResetTimer(int prev_attacks)
	{
		return prev_attacks >= this.MAX_ACTIVE && this.active_attacks.Count < this.MAX_ACTIVE;
	}
	
	/**
	 * Destroy all currently active attacks and projectiles.
	 */
	public void clearAttacks()
	{
		foreach (List<ProjectileBase> attack in this.active_attacks)
		{
			foreach (ProjectileBase proj in attack)
			{
				if (proj != null)
				{
					UnityEngine.Object.Destroy(proj.gameObject);
				}
			}
		}
		this.active_attacks.Clear();
	}
	
	/**
	 * Pause all currently active attacks and projectiles.
	 */
	public void pauseAttacks()
	{
		Time.timeScale = 0;
		foreach (List<ProjectileBase> attack in this.active_attacks)
		{
			foreach (ProjectileBase proj in attack)
			{
				proj.pause();
			}
		}
	}
	
	/**
	 * Unpause all currently active attacks and projectiles.
	 */
	public void unpauseAttacks()
	{
		Time.timeScale = 1;
		foreach (List<ProjectileBase> attack in this.active_attacks)
		{
			foreach (ProjectileBase proj in attack)
			{
				proj.unpause();
			}
		}
	}
	
	private void Awake()
	{
		this.checkAttacks();
		this.checkDelay();
		this.calcTotalWeight();
		this.active_attacks = new List<List<ProjectileBase>>();

		PLAYER_HEALTH = GameObject.FindObjectOfType<Health>();
		PLAYER_SCORE = GameObject.FindObjectOfType<ScoreSystem>();
	}
	
	private void OnAwake()
	{
		this.spawnWave();
	}
	
	private void OnDisable()
	{
		this.clearAttacks();
	}
	
	private void Update()
	{
		int num_active = this.active_attacks.Count;
		this.updateActiveAttacks();
		if (this.shouldResetTimer(num_active))
		{
			this.resetTimer();
		}
		if (this.canSpawnWave())
		{
			this.spawnWave();
		}
	}
}

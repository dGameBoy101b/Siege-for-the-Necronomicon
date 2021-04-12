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
	[SerializeField()]
	[Tooltip("The player object to target attacks on.")]
	public GameObject PLAYER;
	
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
	[Tooltip("The maximum number of active attacks allowed at any one time.\n(Can be infinity for any number of attacks.)")]
	[Min(1)]
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
			&& this.active_attacks.Count < this.MAX_ACTIVE;
	}
	
	/**
	 * Spawn a wave.
	 */
	private void spawnWave()
	{
		System.Random rand = new System.Random();
		this.next_spawn = Time.time + (float)rand.NextDouble() * (this.MAX_DELAY - this.MIN_DELAY) + this.MIN_DELAY;
		this.active_attacks.Add(this.randomAttack());
	}
	
	/**
	 * Spawn a random attack and return the list of projectiles that make up that attack.
	 * @return The list of projectiles spawned in the randomly selected attack.
	 */
	private List<ProjectileBase> randomAttack()
	{
		System.Random rand = new System.Random();
		int r = rand.Next(this.total_weight);
		int weight = 0;
		for (int i = 0; i < this.ATTACK_WEIGHTS.Count; i++)
		{
			weight += this.ATTACK_WEIGHTS[i];
			if (r < weight)
			{
				return this.ATTACK_POOL[i].spawn(this.PLAYER.transform.position, this.PLAYER.transform.rotation);
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
	
	private void Awake()
	{
		this.checkAttacks();
		this.checkDelay();
		this.calcTotalWeight();
	}
	
	private void Update()
	{
		if (this.canSpawnWave())
		{
			this.spawnWave();
		}
	}
}

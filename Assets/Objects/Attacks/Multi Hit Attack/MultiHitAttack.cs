using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * @author Rhys Mader 33705134
 * @date 05/05/2021
 * An attack that spawns one multi hit projectile.
 */
public sealed class MultiHitAttack : AttackBase
{
	[Header("Projectile Prefabs")]
	
	[SerializeField()]
	[Tooltip("The projectile to spawn")]
	public GameObject MULTI_PROJ;
	
	[Header("Speed")]
	
	[SerializeField()]
	[Tooltip("The minimum speed of the spawned projectile in units per second.")]
	[Min(float.Epsilon)]
	public float MIN_SPEED;
	
	[SerializeField()]
	[Tooltip("The maximum speed of the spawned projectile in units per second.")]
	[Min(float.Epsilon)]
	public float MAX_SPEED;
	
	[Header("Distance")]
	
	[SerializeField()]
	[Tooltip("The minimum distance the projectile spawns from the player in units.")]
	[Min(0f)]
	public float MIN_DIST;
	
	[SerializeField()]
	[Tooltip("The maximum distance the projectile spawns from the player in units.")]
	[Min(0f)]
	public float MAX_DIST;
	
	[Header("Longitude")]
	
	[SerializeField()]
	[Tooltip("The minimum angle the player should turn horizontally to face the spawned projectile in degrees.")]
	[Range(0f, 180f)]
	public float MIN_LON;
	
	[SerializeField()]
	[Tooltip("The maximum angle the player should turn horizontally to face the spawned projectile in degrees.")]
	[Range(0f, 180f)]
	public float MAX_LON;
	
	[Header("Latitude")]
	
	[SerializeField()]
	[Tooltip("The minimum angle the player should turn vertically to face the spawned projectile in degrees.")]
	[Range(0f, 90f)]
	public float MIN_LAT;
	
	[SerializeField()]
	[Tooltip("The maximum angle the player should turn vertically to face the spawned projectile in degrees.")]
	[Range(0f, 90f)]
	public float MAX_LAT;
	
	[Header("Hit Capacity")]
	
	[SerializeField()]
	[Tooltip("The minimum hit capacity of the projectile spawned.")]
	[Min(0)]
	public int MIN_HIT;
	
	[SerializeField()]
	[Tooltip("The maximum hit capacity of the projectile spawned.")]
	[Min(0)]
	public int MAX_HIT;
	
	/**
	 * Spawn either a simple magical prjectile or a simple physical projectile.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @param player_health The health of the player.
	 * @param player_score The score of the player.
	 * @return The list of spawned projectiles.
	 */
	public override List<ProjectileBase> spawn(System.Random rng, Vector3 player_pos, Quaternion player_rot, Health player_health, ScoreSystem player_score)
	{
		List<ProjectileBase> projectiles = new List<ProjectileBase>(1);
		MultiHitProjectile proj = GameObject.Instantiate(this.MULTI_PROJ, this.randStartPos(rng, player_pos, player_rot), Quaternion.identity).GetComponent<MultiHitProjectile>();
		proj.SPEED = this.randSpeed(rng);
		proj.TARGET = player_pos;
		proj.PLAYER_HEALTH = player_health;
		proj.PLAYER_SCORE = player_score;
		proj.HIT_CAPACITY = this.randHit(rng);
		proj.spawn();
		projectiles.Add(proj);
		return projectiles;
	}
	
	/**
	 * Randomly generate a hit capacity for a new projectile.
	 * @param rng The random number generator used for all randomisation.
	 * @return The integer random hit capacity of a projectile.
	 */
	private int randHit(System.Random rng)
	{
		return rng.Next(this.MIN_HIT, this.MAX_HIT + 1);
	}
	
	/**
	 * Randomly generate a speed for a new projectile.
	 * @param rng The random number generator used for all randomisation.
	 * @return The float random speed of a projectile in units per second.
	 */
	private float randSpeed(System.Random rng)
	{
		return (float)(rng.NextDouble()) * (this.MAX_SPEED - this.MIN_SPEED) + this.MIN_SPEED;
	}
	
	/**
	 * Randomly generate a distance from the player for a new projectile.
	 * @param rng The random number generator used for all randomisation.
	 * @return The float random distance from the player of a projectile in units.
	 */
	private float randDist(System.Random rng)
	{
		return (float)(rng.NextDouble()) * (this.MAX_DIST - this.MIN_DIST) + this.MIN_DIST;
	}
	
	/**
	 * Randomly ganerate a horzintal angle from the player for a new projectile.
	 * @param rng The random number generator used for all randomisation.
	 * @return The float random horizontal angle from the player in degrees.
	 */
	private float randLon(System.Random rng)
	{
		float lon = (float)(rng.NextDouble()) * (this.MAX_LON - this.MIN_LON) + this.MIN_LON;
		if (rng.Next(1) == 0)
		{
			lon *= -1;
		}
		return lon;
	}
	
	/**
	 * Randomly ganerate a vertical angle from the player for a new projectile.
	 * @param rng The random njumber generator used for alll randomisation.
	 * @return The float random vertical angle from the player in degrees.
	 */
	private float randLat(System.Random rng)
	{
		float lat = (float)(rng.NextDouble()) * (this.MAX_LAT - this.MIN_LAT) + this.MIN_LAT;
		if (rng.Next(1) == 0)
		{
			lat *= -1;
		}
		return lat;
	}
	
	/**
	 * Randomly ganerate a start world position for a new projectile.
	 * @param rng The random number generator used for all randomisation.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The random start world position of a projectile.
	 */
	private Vector3 randStartPos(System.Random rng, Vector3 player_pos, Quaternion player_rot)
	{
		Quaternion lon = Quaternion.AngleAxis(this.randLon(rng), Vector3.up);
		Quaternion lat = Quaternion.AngleAxis(this.randLat(rng), Vector3.right);
		return lon * lat * Vector3.forward * this.randDist(rng) + player_pos;
	}
	
	/**
	 * Check that the minimum speed is lesser than or equal to the maximum speed.
	 * @throws Exception The minimum speed is greater than the maximum speed.
	 */
	private void checkSpeed()
	{
		if (this.MIN_SPEED > this.MAX_SPEED)
		{
			throw new Exception("The minimum speed must be lesser than or equal to the maxiumum speed.");
		}
	}
	
	/**
	 * Check that the minimum distance is lesser than or equal to the maximum distance.
	 * @throws Exception The minimum distance is greater than the maximum distance.
	 */
	private void checkDist()
	{
		if (this.MIN_DIST > this.MAX_DIST)
		{
			throw new Exception("The minimum distance must be lesser than or equal to the maxiumum distance.");
		}
	}
	
	/**
	 * Check that the minimum latitude is lesser than or equal to the maximum latitude.
	 * @throws Exception The minimum latitude is greater than the maximum latitude.
	 */
	private void checkLat()
	{
		if (this.MIN_LAT > this.MAX_LAT)
		{
			throw new Exception("The minimum latitude must be lesser than or equal to the maxiumum latitude.");
		}
	}
	
	/**
	 * Check that the minimum longitude is lesser than or equal to the maximum longitude.
	 * @throws Exception The minimum longitude is greater than the maximum longitude.
	 */
	private void checkLon()
	{
		if (this.MIN_LON > this.MAX_LON)
		{
			throw new Exception("The minimum longitude must be lesser than or equal to the maxiumum longitude.");
		}
	}
	
	/**
	 * Check that the minimum hit capacity is lesser than or equal to the maximum hit capacity.
	 * @throws Exception The minimum hit capacity is greater than the maximum hit capacity.
	 */
	private void checkHit()
	{
		if (this.MIN_HIT > this.MAX_HIT)
		{
			throw new Exception("The minimum hit capacity must be lesser than or equal to the maxiumum hit capacity.");
		}
	}
	
	private void Awake()
	{
		this.checkSpeed();
		this.checkDist();
		this.checkLon();
		this.checkLat();
		this.checkHit();
	}
}

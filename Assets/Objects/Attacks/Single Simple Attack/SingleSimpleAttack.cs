using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * @author Rhys Mader 33705134
 * @date 13/04/2021
 * An attack that spawns a single simple projectile.
 */
public sealed class SingleSimpleAttack : AttackBase
{
	[Header("Projectile Prefabs")]
	
	[SerializeField()]
	[Tooltip("The simple magical projectile prefab.")]
	public GameObject MAGIC_PROJ;
	
	[SerializeField()]
	[Tooltip("The simple physical projectile prefab.")]
	public GameObject PHYSICS_PROJ;
	
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
	
	/**
	 * Spawn either a simple magical prjectile or a simple physical projectile.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The list of spawned projectiles.
	 */
	public override List<ProjectileBase> spawn(Vector3 player_pos, Quaternion player_rot)
	{
		List<ProjectileBase> projectiles = new List<ProjectileBase>(1);
		System.Random rand = new System.Random();
		if (rand.Next(2) == 0)
		{
			projectiles.Add(this.spawnMagical(player_pos, player_rot));
		}
		else
		{
			projectiles.Add(this.spawnPhysical(player_pos, player_rot));
		}
		return projectiles;
	}
	
	/**
	 * Randomly generate a speed for a new projectile.
	 * @return The float random speed of a projectile in units per second.
	 */
	private float randSpeed()
	{
		return (float)(new System.Random().NextDouble()) * (this.MAX_SPEED - this.MIN_SPEED) + this.MIN_SPEED;
	}
	
	/**
	 * Randomly generate a distance from the player for a new projectile.
	 * @return The float random distance from the player of a projectile in units.
	 */
	private float randDist()
	{
		return (float)(new System.Random().NextDouble()) * (this.MAX_DIST - this.MIN_DIST) + this.MIN_DIST;
	}
	
	/**
	 * Randomly ganerate a horzintal angle from the player for a new projectile.
	 * @return The float random horizontal angle from the player in degrees.
	 */
	private float randLon()
	{
		float lon = (float)(new System.Random().NextDouble()) * (this.MAX_LON - this.MIN_LON) + this.MIN_LON;
		if (new System.Random().Next(1) == 0)
		{
			lon *= -1;
		}
		return lon;
	}
	
	/**
	 * Randomly ganerate a vertical angle from the player for a new projectile.
	 * @return The float random vertical angle from the player in degrees.
	 */
	private float randLat()
	{
		float lat = (float)(new System.Random().NextDouble()) * (this.MAX_LAT - this.MIN_LAT) + this.MIN_LAT;
		if (new System.Random().Next(1) == 0)
		{
			lat *= -1;
		}
		return lat;
	}
	
	/**
	 * Randomly ganerate a start world position for a new projectile.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The random start world position of a projectile.
	 */
	private Vector3 randStartPos(Vector3 player_pos, Quaternion player_rot)
	{
		Quaternion lon = Quaternion.AngleAxis(this.randLon(), Vector3.up);
		Quaternion lat = Quaternion.AngleAxis(this.randLat(), Vector3.right);
		return lon * lat * player_rot * Vector3.forward * this.randDist() + player_pos;
	}
	
	/**
	 * Spawn a simple magical projectile.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The script attached to the simple magical projectile.
	 */
	private SimpleMagicalProjectile spawnMagical(Vector3 player_pos, Quaternion player_rot)
	{
		SimpleMagicalProjectile proj = GameObject.Instantiate(this.MAGIC_PROJ, this.randStartPos(player_pos, player_rot), Quaternion.identity).GetComponent<SimpleMagicalProjectile>();
		proj.SPEED = this.randSpeed();
		proj.TARGET = player_pos;
		return proj;
	}
	
	/**
	 * Spawn a simple physical projectile.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The script attached to the simple physcial projectile.
	 */
	private SimplePhysicalProjectile spawnPhysical(Vector3 player_pos, Quaternion player_rot)
	{
		SimplePhysicalProjectile proj = GameObject.Instantiate(this.PHYSICS_PROJ, this.randStartPos(player_pos, player_rot), Quaternion.identity).GetComponent<SimplePhysicalProjectile>();
		proj.SPEED = this.randSpeed();
		proj.TARGET = player_pos;
		return proj;
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
	
	private void Awake()
	{
		this.checkSpeed();
		this.checkDist();
		this.checkLon();
		this.checkLat();
	}
}

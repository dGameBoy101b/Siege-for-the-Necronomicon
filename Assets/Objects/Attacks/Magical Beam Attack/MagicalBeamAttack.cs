using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
 * @author Rhys Mader 33705134
 * @date 04/05/2021
 * An attack that creates one magical beam.
 */
public sealed class MagicalBeamAttack : AttackBase
{
	[Header("Prefabs")]
	
	[SerializeField()]
	[Tooltip("The magic beam prefab to spawn.")]
	public GameObject BEAM;
	
	[Header("Distance")]
	
	[SerializeField()]
	[Tooltip("The minimum distance the projectile spawns from the player in units.\n(May be ignored to ensure the projectile can be seen by the player)")]
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
	
	[Header("Charge Time")]
	
	[SerializeField()]
	[Tooltip("The minimum number of seconds the spawned projectile waits before attacking.")]
	[Min(0)]
	public float MIN_CHARGE;
	
	[SerializeField()]
	[Tooltip("The maximum number of seconds the spawned projectile waits before attacking.")]
	[Min(0)]
	public float MAX_CHARGE;
	
	[Header("Attack Time")]
	
	[SerializeField()]
	[Tooltip("The minimum number of seconds the spawned projectile keeps attacking for.")]
	[Min(0)]
	public float MIN_ATTACK;
	
	[SerializeField()]
	[Tooltip("The maximum number of seconds the spawned projectile keeps attacking for.")]
	[Min(0)]
	public float MAX_ATTACK;
	
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
		MagicBeam proj = GameObject.Instantiate(this.BEAM, this.randStartPos(rng, player_pos, player_rot), Quaternion.identity).GetComponent<MagicBeam>();
		proj.TARGET = player_pos;
		proj.PLAYER_HEALTH = player_health;
		proj.PLAYER_SCORE = player_score;
		proj.CHARGE_TIME = this.randCharge(rng);
		proj.STAY_TIME = this.randAttack(rng);
		proj.spawn();
		projectiles.Add(proj);
		return projectiles;
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
	 * Randomly ganerate a start world position for a new projectile that will be visible to the player.
	 * @param rng The random number generator used for all randomisation.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The random start world position of a projectile.
	 */
	private Vector3 randStartPos(System.Random rng, Vector3 player_pos, Quaternion player_rot)
	{
		Quaternion lon = Quaternion.AngleAxis(this.randLon(rng), Vector3.up);
		Quaternion lat = Quaternion.AngleAxis(this.randLat(rng), Vector3.right);
		Vector3 dir = lon * lat * player_rot * Vector3.forward;
		float dist = this.randDist(rng);
		RaycastHit hit;
		if (Physics.Raycast(player_pos, dir, out hit, dist, LayerMask.GetMask(new string[]{"Default", "Level Bounds"}), QueryTriggerInteraction.Ignore))
		{
			return hit.point;
		}
		return dir * dist + player_pos;
	}
	
	/**
	 * Randomly ganerate the number of seconds a new beam waits before attacking.
	 * @param rng The random number generator used for all randomisation.
	 * @return The float random number of seconds a new beam waits before attacking.
	 */
	private float randCharge(System.Random rng)
	{
		return (float)rng.NextDouble() * (this.MAX_CHARGE - this.MIN_CHARGE) + this.MIN_CHARGE;
	}
	
	/**
	 * Randomly ganerate the number of seconds a new beam attacks for.
	 * @param rng The random number generator used for all randomisation.
	 * @return The float random number of seconds a new beam attacks for.
	 */
	private float randAttack(System.Random rng)
	{
		return (float)rng.NextDouble() * (this.MAX_ATTACK - this.MIN_ATTACK) + this.MIN_ATTACK;
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
	 * Check that the minimum charge time is lesser than or equal to the maximum charge time.
	 * @throws Exception The minimum charge time is greater than the maximum charge time.
	 */
	private void checkCharge()
	{
		if (this.MIN_CHARGE > this.MAX_CHARGE)
		{
			throw new Exception("The minimum charge time must be lesser then or equal to the maximum charge time.");
		}
	}
	
	/**
	 * Check that the minimum attack time is lesser than or equal to the maximum attack time.
	 * @throws Exception The minimum attack time is greater than the maximum attack time.
	 */
	private void checkAttack()
	{
		if (this.MIN_ATTACK > this.MAX_ATTACK)
		{
			throw new Exception("The minimum attack time must be lesser then or equal to the maximum attack time.");
		}
	}
	
	private void Awake()
	{
		this.checkDist();
		this.checkLon();
		this.checkLat();
		this.checkCharge();
		this.checkAttack();
	}
}

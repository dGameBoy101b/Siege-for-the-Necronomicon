using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * A simple projectile that moves in a straight line towards a target position at a constant velocity.
 */
public sealed class SimpleMagicalProjectile : MagicalProjectileBase
{
	[Header("Projectile Attributes")]
	
	[SerializeField()]
	[Tooltip("The amount of damage to do to the player when hit.")]
	[Min(0)]
	public int DAMAGE;
	
	[SerializeField()]
	[Tooltip("The number of point to add to the score when this projectile is blocked.")]
	[Min(0)]
	public int POINTS;
	
	[SerializeField()]
	[Tooltip("The global position this simple magical projectile will fly towards.")]
	public Vector3 TARGET;
	
	[SerializeField()]
	[Tooltip("The speed in units per second at which this simple magical projectile moves forward.")]
	[Min(float.Epsilon)]
	public float SPEED;

	[Header("Physical Projectile Animation")]

	[SerializeField()]
	[Tooltip("Is the effects that plays when the projectile is defeated.")]
	public GameObject defeatAnimation;

	[SerializeField()]
	[Tooltip("Is the effects that plays when the projectile is hits the player.")]
	public GameObject attackAnimation;

	/**
	 * Rotate this simple projectile to face the target.
	 */
	private void faceTarget()
	{
		this.transform.rotation = Quaternion.LookRotation(this.TARGET - this.transform.position);
	}
	
	/**
	 * Move this simple projectile forwards.
	 * @param t The float number of seconds since this function was called.
	 */
	private void moveForward(float t)
	{
		this.transform.position += this.transform.forward * this.SPEED * t;
	}
	
	/**
	 * Attack the player when hit.
	 */
	public override void attack()
	{
		this.PLAYER_HEALTH.TakeDamage(DAMAGE);
		this.playAttackAnimation();
		base.attack();
	}
	
	/**
	 * Destroy this projectile when it hits the gauntlet.
	 */
	public override void defeat()
	{
		this.PLAYER_SCORE.AddScore(this.POINTS);
		this.playDefeatAnimation();
		base.defeat();
	}
	
	private void Start()
	{
		this.faceTarget();
	}
	
	private void Update()
	{
		this.moveForward(Time.deltaTime);
	}

	/**
	 * @author Allan Zheng 33690777
	 * Plays the animation for when the player defeats a projectile.
	 */
	private void playDefeatAnimation()
	{
		Instantiate(defeatAnimation, transform.position, transform.rotation);
	}

	/**
	 * @author Allan Zheng 33690777
	 * Plays the animation for when the player is hit by a projectile.
	 */
	private void playAttackAnimation()
	{
		Instantiate(attackAnimation, transform.position, transform.rotation);
	}
}

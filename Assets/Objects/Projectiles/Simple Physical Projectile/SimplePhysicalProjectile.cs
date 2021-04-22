using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * A simple physical projectile that moves in a straight line towards a target position at a constant velocity.
 */
public sealed class SimplePhysicalProjectile : PhysicalProjectileBase
{
	[SerializeField()]
	[Tooltip("The amount of damage to do to the player when hit.")]
	[Min(0)]
	public int DAMAGE;
	
	[SerializeField()]
	[Tooltip("The number of points to add to the player's score when this projectile is defeated.")]
	[Min(0)]
	public int POINTS;
	
	[SerializeField()]
	[Tooltip("The global position this simple physical projectile will fly towards.")]
	public Vector3 TARGET;
	
	[SerializeField()]
	[Tooltip("The speed in units per second at which this simple physical projectile moves forward.")]
	[Min(float.Epsilon)]
	public float SPEED;
	
	/**
	 * Rotate this simple physical projectile to face the target.
	 */
	private void faceTarget()
	{
		this.transform.rotation = Quaternion.LookRotation(this.TARGET - this.transform.position);
	}
	
	/**
	 * Move this simple physical projectile forwards.
	 * @param t The float number of seconds since this function was called.
	 */
	private void moveForward(float t)
	{
		this.transform.position += this.transform.forward * this.SPEED * t;
	}
	
	/**
	 * Destroy this projectile when it is hit by a sword slash.
	 */
	public override void hit()
	{
		this.PLAYER_SCORE.AddScore(this.POINTS);
		Object.Destroy(this.gameObject);
	}
	
	/**
	 * The damage the player and destroy itself when this projectile hits the player.
	 */
	public override void attack()
	{
		//Added by Allan for Audio Reasons
		FindObjectOfType<AudioManager>().Play("Hurt");
		this.PLAYER_HEALTH.TakeDamage(this.DAMAGE);
		Object.Destroy(this.gameObject);
	}
	
	private void Start()
	{
		this.faceTarget();
	}
	
	private void Update()
	{
		this.moveForward(Time.deltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePhysicalProjectile : PhysicalProjectileBase
{
	[Header("Projectile Attributes")]

	[SerializeField()]
	[Tooltip("The amount of damage to do to the player when hit.")]
	[Min(0)]
	public int DAMAGE;

	[SerializeField()]
	[Tooltip("The number of point to add to the score when this simple physical projectile is slashed.")]
	[Min(0)]
	public int POINTS;

	[SerializeField()]
	[Tooltip("The global position this simple physical projectile will fly towards.")]
	public Vector3 TARGET;

	[SerializeField()]
	[Tooltip("The speed in units per second at which this simple physical projectile moves forward.")]
	[Min(float.Epsilon)]
	public float SPEED;

	[Header("Physical Projectile Animation")]

	[SerializeField()]
	[Tooltip("Is the fractured version of the projectile game object.")]
	public GameObject defeatedPhysicalProjectile;

	[SerializeField()]
	[Tooltip("Is the force of the fractured game object's explosion.")]
	public float breakForce;

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
	 * Attack the player when hit.
	 */
	public override void attack()
	{
		this.PLAYER_HEALTH.TakeDamage(DAMAGE);
		this.hitAnimation();
		base.attack();
	}
	
	/**
	 * Destroy this simple physical projectile when it hits a sword slash.
	 */
	public override void defeat()
	{
		this.PLAYER_SCORE.AddScore(this.POINTS);
		this.hitAnimation();
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
	 * Plays the animation for when the player is hit by a / defeats projectile.
	 */
	private void hitAnimation()
	{
		GameObject defeatedAnimaton = Instantiate(defeatedPhysicalProjectile, transform.position, transform.rotation);

		foreach (Rigidbody rb in defeatedAnimaton.GetComponentsInChildren<Rigidbody>())
		{
			Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;
			rb.AddForce(force);
		}

		Destroy(defeatedAnimaton, 10);
	}
}

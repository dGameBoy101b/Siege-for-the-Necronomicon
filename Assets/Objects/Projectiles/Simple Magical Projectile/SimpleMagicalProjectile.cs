using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * A simple magical projectile that moves in a straight line towards a target position at a constant velocity.
 */
public sealed class SimpleMagicalProjectile : MagicalProjectileBase
{
	[SerializeField()]
	[Tooltip("The global position this simple magical projectile will fly towards.")]
	public Vector3 TARGET;
	
	[SerializeField()]
	[Tooltip("The speed in units per second at which this simple magical projectile moves forward.")]
	[Min(float.Epsilon)]
	public float SPEED;
	
	/**
	 * Rotate this simple magical projectile to face the target.
	 */
	private void faceTarget()
	{
		this.transform.rotation = Quaternion.LookRotation(this.TARGET - this.transform.position);
	}
	
	/**
	 * Move this simple magical projectile forwards.
	 * @param t The float number of seconds since this function was called.
	 */
	private void moveForward(float t)
	{
		this.transform.position += Vector3.forward * this.SPEED * t;
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

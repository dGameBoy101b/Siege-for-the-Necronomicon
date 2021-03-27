﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * A simple physical projectile that moves in a straight line towards a target position at a constant velocity.
 */
public sealed class SimplePhysicalProjectile : PhysicalProjectileBase
{
	/**
	 * Construct a new simple physical projectile with the given attributes.
	 * @param speed The float speed in units per second at which this projectile moves forward.
	 * @param target The global position this simple physical protectile is aimed at.
	 */
	public SimplePhysicalProjectile(float speed, Vector3 target)
	{
		this.Speed = speed;
		this.Target = target;
	}
	
	/**
	 * The global position this simple physical projectile willl fly towards.
	 */
	public Vector3 Target {get; private set;}
	
	/**
	 * The speed in units per second at which this simple physical projectile moves forward.
	 */
	public float Speed {get; private set;}
	
	/**
	 * Rotate this simple physical projectile to face the target.
	 */
	private void faceTarget()
	{
		this.transform.rotation = Quaternion.LookRotation(this.Target - this.transform.position);
	}
	
	/**
	 * Move this simple physical projectile forwards.
	 * @param t The float number of seconds since this function was called.
	 */
	private void moveForward(float t)
	{
		this.transform.position += Vector3.forward * this.Speed * t;
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

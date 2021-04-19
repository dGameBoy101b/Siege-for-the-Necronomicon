using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * The abstract base class for all projectiles that attack the player.
 */
public abstract class ProjectileBase : MonoBehaviour
{
	/**
	 * The names of the collision layers that should destroy projectiles when they collide.
	 */
	public static string[] DESTROY_LAYERS {get;} = {"Default"};
	
	/**
	 * Test if the given collider is part of a collision layer projectiles should destroy themselves against.
	 * @param col The collider to test.
	 * @return True if the given collider should destroy projectiles, false otherwise.
	 */
	public static bool shouldBeDestroyed(Collider col)
	{
		return (LayerMask.GetMask(ProjectileBase.DESTROY_LAYERS) 
		        & LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Ensure all colliders of this projectile are triggers.
	 */
	private void setTriggers()
	{
		foreach (Collider col in this.gameObject.GetComponents<Collider>())
		{
			col.isTrigger = true;
		}
	}
	
	/**
	 * Set the collision layer of this projectile.
	 */
	protected abstract void setCollisionLayer();
	
	protected virtual void OnTriggerEnter(Collider col)
	{
		if (ProjectileBase.shouldBeDestroyed(col))
		{
			Object.Destroy(this.gameObject);
		}
	}
	
	protected virtual void Awake()
	{
		this.setCollisionLayer();
		this.setTriggers();
	}
}

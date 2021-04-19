using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * The abstract base class for all magical projectiles that must be blocked by the player.
 */
public abstract class MagicalProjectileBase : ProjectileBase
{
	/**
	 * The name of the collision layer for all magical projectiles.
	 */
	public static string COL_LAYER {get;} = "Magical Projectile";
	
	/**
	 * The names of the collision layers that register a successful hit on all magical projectiles.
	 */
	public static string[] HIT_LAYERS {get;} = {"Gauntlet"};
	
	/**
	 * Test if the given collider should be counted as a successful hit on a magical projectile.
	 * @param col The collider a magical projectile hit.
	 * @return True if the given collider should count as a successful hit, false otherwise.
	 */
	public static bool shouldHit(Collider col)
	{
		return (LayerMask.GetMask(MagicalProjectileBase.HIT_LAYERS) 
			& LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Set the collision layer of this magical projectile.
	 */
	protected override void setCollisionLayer()
	{
		this.gameObject.layer = LayerMask.NameToLayer(MagicalProjectileBase.COL_LAYER);
	}
	
	/**
	 * The function called when this magical projectile is successfully hit.
	 */
	public abstract void hit();
	
	protected override void OnTriggerEnter(Collider col)
	{
		base.OnTriggerEnter(col);
		
		if (MagicalProjectileBase.shouldHit(col))
		{
			this.hit();
		}
	}
}

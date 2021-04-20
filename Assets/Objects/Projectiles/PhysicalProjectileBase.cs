using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * The abstract base class for all physical projectiles that must be slashed by the player.
 */
public abstract class PhysicalProjectileBase : ProjectileBase
{
	/**
	 * The name of the collision layer for all physical projectiles.
	 */
	public static string COL_LAYER {get;} = "Physical Projectile";
	
	/**
	 * The names of the collision layers that register a successful hit on all physical projectiles.
	 */
	public static string[] HIT_LAYERS {get;} = {"Sword Slash"};
	
	/**
	 * Test if the given collider should be counted as a successful hit on a physical projectile.
	 * @param col The collider a physical projectile hit.
	 * @return True if the given collider should count as a successful hit, false otherwise.
	 */
	public static bool shouldHit(Collider col)
	{
		return (LayerMask.GetMask(PhysicalProjectileBase.HIT_LAYERS) 
			& LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Set the collision layer of this physical projectile.
	 */
	protected override void setCollisionLayer()
	{
		this.gameObject.layer = LayerMask.NameToLayer(PhysicalProjectileBase.COL_LAYER);
	}
	
	/**
	 * The function called when this physical projectile is successfully hit.
	 */
	public abstract void hit();
	
	protected override void OnTriggerEnter(Collider col)
	{
		base.OnTriggerEnter(col);
		
		if (PhysicalProjectileBase.shouldHit(col))
		{
			this.hit();
		}
	}
}

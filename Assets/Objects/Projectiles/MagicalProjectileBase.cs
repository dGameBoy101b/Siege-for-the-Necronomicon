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
	 * Set the collision layer of this magical projectile.
	 */
	protected override void setCollisionLayer()
	{
		this.gameObject.layer = LayerMask.NameToLayer(MagicalProjectileBase.COL_LAYER);
	}
}

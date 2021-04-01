using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 27/03/2021
 * The abstract base class for all physical projectiles that must be slashed by the player.
 */
public class PhysicalProjectileBase : ProjectileBase
{
	/**
	 * The name of the collision layer for all physical projectiles.
	 */
	public static string COL_LAYER {get;} = "Physical Projectile";
	
	/**
	 * Set the collision layer of this physical projectile.
	 */
	protected override void setCollisionLayer()
	{
		this.gameObject.layer = LayerMask.NameToLayer(PhysicalProjectileBase.COL_LAYER);
	}
}

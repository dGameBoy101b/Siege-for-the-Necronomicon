using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicalProjectileBase : ProjectileBase
{
	/**
	 * The names of the collision layers that magical projectiles should be defeated by.
	 */
	public override string[] DEFEAT_LAYERS {get;} = {"Gauntlet"};
}

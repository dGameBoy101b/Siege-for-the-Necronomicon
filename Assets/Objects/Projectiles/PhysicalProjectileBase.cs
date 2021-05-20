using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalProjectileBase : ProjectileBase
{
    /**
	 * The names of the collision layers that physical projectiles should be defeated by.
	 */
	public override string[] DEFEAT_LAYERS {get;} = {"Sword Slash", "Sword"};
}

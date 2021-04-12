using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour
{
	/**
	 * Spawn all the projectiles in this attack.
	 * @return The list of spawned projectiles.
	 */
	public abstract List<ProjectileBase> spawn();
}

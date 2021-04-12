﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 13/04/2021
 * An abstract base class for attacks the wave manager spawns.
 */
public abstract class AttackBase : MonoBehaviour
{
	/**
	 * Spawn all the projectiles in this attack.
	 * @param player_pos The current world position of the player.
	 * @param player_rot The current world rotation of the player.
	 * @return The list of spawned projectiles.
	 */
	public abstract List<ProjectileBase> spawn(Vector3 player_pos, Quaternion player_rot);
}

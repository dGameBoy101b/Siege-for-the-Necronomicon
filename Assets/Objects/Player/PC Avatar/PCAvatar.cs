using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 19/04/2021
 * A class to auto control the player when playing the PC build.
 */
public sealed class PCAvatar : MonoBehaviour
{
	[Header("Equipment")]
	
	[SerializeField()]
	[Tooltip("The sword to control with this player.")]
	public Sword SWORD;
	
	[SerializeField()]
	[Tooltip("The gauntlet to control woth this player.")]
	public Gauntlet GAUNTLET;
	
	[Header("Controls")]

	[SerializeField()]
	[Tooltip("The input manager name for the button to use to activate the sword.")]
	public string SWORD_CONTROL;
	
	[SerializeField()]
	[Tooltip("The input manager name for the buttone to use to activate the gauntlet.")]
	public string GAUNTLET_CONTROL;
}

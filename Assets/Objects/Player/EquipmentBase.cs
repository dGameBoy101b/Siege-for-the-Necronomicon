using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipmentBase : MonoBehaviour
{
	[Header("Player")]
	
	[SerializeField()]
	[Tooltip("The player's center.")]
	public Transform PLAYER;
	
	[SerializeField()]
	[Tooltip("The object to attach this object to when the left hand is dominant.")]
	public Transform LEFT_HAND;
	
	[SerializeField()]
	[Tooltip("The object to attach this object to when the right hand is dominant.")]
	public Transform RIGHT_HAND;
	
	[SerializeField()]
	[Tooltip("The object to attach this object to when there is no dominant hand.")]
	public Transform DEFAULT_HAND;
	
	/**
	 * Update the hand this object is attached to.
	 */
	public void updateHandedness()
	{
		Transform hand;
		switch (OVRInput.GetDominantHand())
		{
			case OVRInput.Handedness.LeftHanded:
				hand = this.LEFT_HAND;
				break;
			case OVRInput.Handedness.RightHanded:
				hand = this.RIGHT_HAND;
				break;
			default:
				hand = this.DEFAULT_HAND;
				break;
		}
		this.transform.SetParent(hand, false);
	}
	
	protected virtual void Start()
	{
		this.updateHandedness();
	}
}

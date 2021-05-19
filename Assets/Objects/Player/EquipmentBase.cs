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
	
	[Header("Options")]
	
	[SerializeField()]
	[Tooltip("The name of the option which indicate if the player is left handed.")]
	public string HANDEDNESS;
	
	/**
	 * Update the hand this object is attached to.
	 */
	public void updateHandedness()
	{
		Transform hand;
		try
		{
			if((bool)OptionStore.Instance.getOption(this.HANDEDNESS))
			{
				hand = this.LEFT_HAND;
			}
			else
			{
				hand = this.RIGHT_HAND;
			}
		}
		catch (OptionStore.OptionDoesNotExistException)
		{
			Debug.LogWarning("Could not find handedness option. Falling back on OVRInput.");
			switch (OVRInput.GetDominantHand())
			{
				case OVRInput.Handedness.LeftHanded:
					hand = this.LEFT_HAND;
					break;
				case OVRInput.Handedness.RightHanded:
					hand = this.RIGHT_HAND;
					break;
				default:
					Debug.LogWarning("No OVRInput handedness found.");
					hand = this.DEFAULT_HAND;
					break;
			}
		}
		this.transform.SetParent(hand, false);
	}
	
	protected virtual void Start()
	{
		this.updateHandedness();
	}

	protected virtual void Update() 
	{
		
	}
}

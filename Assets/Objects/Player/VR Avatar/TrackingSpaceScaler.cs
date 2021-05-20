using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 13/05/2021
 * A class that scales the VR tracking space in response to adjustment settings.
 */
public sealed class TrackingSpaceScaler : MonoBehaviour
{
	[Header("Object References")]
	
	[SerializeField()]
	[Tooltip("The game object to rescale that represents the VR tracking space.")]
	public GameObject TRACKING_SPACE;
	
	[Header("Option Names")]
	
	[SerializeField()]
	[Tooltip("The name of the reach adjustment option.")]
	public string REACH;
	
	[SerializeField()]
	[Tooltip("The name of the height adjustment option.")]
	public string HEIGHT;

	/**
	 * The original local scale of the linked tracking space.
	 */
	private Vector3 origin_scale;

	/**
	 * The original local position of the linked tracking space.
	 */
	private Vector3 origin_pos;
	
	/**
	 * Rescale the linked tracking space to account for the reach adjustment.
	 */
	private void scaleReach()
	{
		float reach = (float)OptionStore.Instance.getOption(this.REACH);
		if (reach < 0f)
		{
			reach = -1f / reach;
		}
		else
		{
			reach += 1f;
		}
		this.TRACKING_SPACE.transform.localScale = Vector3.Scale(this.origin_scale, reach * Vector3.one);
	}
	
	/**
	 * Reposition the linked tracking space for the height adjustment.
	 */
	private void translateHeight()
	{
		float height = (float)OptionStore.Instance.getOption(this.HEIGHT);
		this.TRACKING_SPACE.transform.localPosition = this.origin_pos + height * Vector3.up;
	}
	
	private void Start()
	{
		this.origin_pos = this.TRACKING_SPACE.transform.localPosition;
		this.origin_scale = this.TRACKING_SPACE.transform.localScale;
		this.scaleReach();
		this.translateHeight();
	}
}

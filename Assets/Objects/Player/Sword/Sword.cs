using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Sword : EquipmentBase
{
	[Header("Slashing")]
	
	[SerializeField()]
	[Tooltip("The sword slash prefab to spawn.")]
	public GameObject SLASH_PREFAB;
	
	[SerializeField()]
	[Tooltip("The offset from this game object that sword slashes should be generated.")]
	public Vector3 SLASH_OFFSET;
	
	[SerializeField()]
	[Tooltip("The VR controller button used to draw a slash.")]
	public OVRInput.Button SLASH_BUTTON;
	
	/**
	 * The global point at which the next slash should start.
	 */
	private Vector3 slash_start;
	
	/**
	 * Create a slash between the given global points facing away from the player.
	 * @param start The global point at which the slash starts.
	 * @param end The global point at which the slash ends.
	 * @param speed_scale The float units per second to scale the speed of the slash by, defaults to 1 (no effect).
	 */
	public GameObject createSlash(Vector3 start, Vector3 end, float speed_scale = 1f)
	{
		Vector3 midpoint = Vector3.Lerp(start, end, 0.5f);
		GameObject slash = GameObject.Instantiate(this.SLASH_PREFAB, midpoint, Quaternion.LookRotation(this.PLAYER.position - midpoint, end - start));
		slash.transform.localScale = Vector3.Scale(new Vector3(1f, Vector3.Distance(start, end) / slash.GetComponent<Collider>().bounds.size.y, 1f), slash.transform.localScale);
		slash.GetComponent<SwordSlash>().SPEED *= speed_scale;
		return slash;
	}
	
	/**
	 * Called when a slash starts.
	 */
	private void startSlash()
	{
		this.slash_start = this.transform.position + this.SLASH_OFFSET;
	}
	
	/**
	 * Called when a slash ends.
	 */
	private void endSlash()
	{
		this.createSlash(this.slash_start, this.transform.position + this.SLASH_OFFSET);
		Debug.Log("End slash " + this.transform.position);
	}
	
	protected override void Update()
	{
		if (/*(OVRInput.GetDominantHand() == OVRInput.Handedness.LeftHanded && OVRInput.GetDown(this.SLASH_BUTTON, OVRInput.Controller.LHand))
			|| (OVRInput.GetDominantHand() == OVRInput.Handedness.RightHanded && OVRInput.GetDown(this.SLASH_BUTTON, OVRInput.Controller.RHand))
			|| (OVRInput.GetDominantHand() == OVRInput.Handedness.Unsupported && OVRInput.GetDown(this.SLASH_BUTTON, OVRInput.Controller.Active))*/
			OVRInput.GetDown(this.SLASH_BUTTON))
		{
			this.startSlash();
			Debug.Log("start slash position " + slash_start);
		}
		if (/*(OVRInput.GetDominantHand() == OVRInput.Handedness.LeftHanded && OVRInput.GetUp(this.SLASH_BUTTON, OVRInput.Controller.LHand))
			|| (OVRInput.GetDominantHand() == OVRInput.Handedness.RightHanded && OVRInput.GetUp(this.SLASH_BUTTON, OVRInput.Controller.RHand))
			|| (OVRInput.GetDominantHand() == OVRInput.Handedness.Unsupported && OVRInput.GetUp(this.SLASH_BUTTON, OVRInput.Controller.Active))*/
			OVRInput.GetUp(this.SLASH_BUTTON))
		{
			this.endSlash();
		}
	}
}

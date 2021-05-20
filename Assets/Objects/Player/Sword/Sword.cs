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
	[Tooltip("The amount in degrees to rotate the created slash")]
	public float SLASH_ROTATION_OFFSET;
	
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
		if(end.x > start.x)
		{
			Vector3 temp = start;
			start = end;
			end = temp;
		}
		GameObject slash = GameObject.Instantiate(this.SLASH_PREFAB, midpoint, Quaternion.LookRotation(midpoint - this.PLAYER.position, end - start));
		//slash.transform.localScale = Vector3.Scale(new Vector3(Vector3.Distance(start, end) / slash.GetComponent<Collider>().bounds.size.x, Vector3.Distance(start, end) / slash.GetComponent<Collider>().bounds.size.y, 1f), slash.transform.localScale);
		//slash.transform.Rotate(0f, SLASH_ROTATION_OFFSET, 0f, Space.Self);
		slash.GetComponent<SwordSlash>().SPEED *= speed_scale;
		return slash;
	}
	
	/**
	 * Called when a slash starts.
	 */
	private void startSlash()
	{
		this.slash_start = this.transform.position + this.transform.rotation * this.SLASH_OFFSET;
		GameObject start_point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Object.DestroyImmediate(start_point.GetComponent<Collider>());
	}
	
	/**
	 * Called when a slash ends.
	 */
	private void endSlash()
	{
		this.createSlash(this.slash_start, this.transform.position + this.transform.rotation * this.SLASH_OFFSET);
		GameObject start_point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		Object.DestroyImmediate(start_point.GetComponent<Collider>());
	}
	
	protected override void Update()
	{
		if (OVRInput.GetDown(this.SLASH_BUTTON))
		{
			this.startSlash();
		}
		if (OVRInput.GetUp(this.SLASH_BUTTON))
		{
			this.endSlash();
		}
	}
}

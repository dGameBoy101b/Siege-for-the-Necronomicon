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
	[Tooltip("The VR controller button used to draw a slash when left handed.")]
	public OVRInput.Button LEFT_SLASH_BUTTON;

	[SerializeField()]
	[Tooltip("The VR controller button used to draw a slash when right handed.")]
	public OVRInput.Button RIGHT_SLASH_BUTTON;

	[SerializeField()]
	[Tooltip("The VR controller button used to draw a slash when neither right nor left handed.")]
	public OVRInput.Button DEFAULT_SLASH_BUTTON;

	/**
	 * The global point at which the next slash should start.
	 */
	private Vector3 slash_start;

	/**
	 * The button to use for creating sword slashes.
	 */
	private OVRInput.Button slash_button;
	
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
		slash.transform.localScale = new Vector3(1f, Vector3.Distance(start, end), 1f);
		slash.GetComponent<SwordSlash>().SPEED *= speed_scale;
		return slash;
	}
	
	/**
	 * Called when a slash starts.
	 */
	private void startSlash()
	{
		if (this.start_point != null)
        {
			GameObject.DestroyImmediate(this.start_point);
        }
		if (this.end_point != null)
        {
			GameObject.DestroyImmediate(this.end_point);
        }
		this.slash_start = this.transform.position + (this.transform.rotation * this.SLASH_OFFSET);
		this.start_point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		this.start_point.transform.position = this.slash_start;
		this.start_point.transform.localScale = Vector3.one * .1f;
		Object.DestroyImmediate(this.start_point.GetComponent<Collider>());
	}

	private GameObject start_point;
	private GameObject end_point = null;
	
	/**
	 * Called when a slash ends.
	 */
	private void endSlash()
	{
		Vector3 slash_end = this.transform.position + (this.transform.rotation * this.SLASH_OFFSET);
		this.createSlash(this.slash_start, slash_end);
		this.end_point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		this.end_point.transform.position = slash_end;
		this.end_point.transform.localScale = Vector3.one * .1f;
		this.end_point.GetComponent<Renderer>().material.color = Color.red;
		Object.DestroyImmediate(this.end_point.GetComponent<Collider>());
	}
	
	protected override void Update()
	{
		if (OVRInput.GetDown(this.slash_button))
		{
			this.startSlash();
		}
		if (OVRInput.GetUp(this.slash_button))
		{
			this.endSlash();
		}
	}

    protected override void Start()
    {
        base.Start();
		this.updateButtonHandedness();
    }

    /**
	 * Update the button this sword use to create slashes.
	 */
    public void updateButtonHandedness()
	{
		try
		{
			if ((bool)OptionStore.Instance.getOption(this.HANDEDNESS))
			{
				this.slash_button = this.LEFT_SLASH_BUTTON;
			}
			else
			{
				this.slash_button = this.RIGHT_SLASH_BUTTON;
			}
		}
		catch (OptionStore.OptionDoesNotExistException)
		{
			Debug.LogWarning("Could not find handedness option. Falling back on OVRInput.");
			switch (OVRInput.GetDominantHand())
			{
				case OVRInput.Handedness.LeftHanded:
					this.slash_button = this.LEFT_SLASH_BUTTON;
					break;
				case OVRInput.Handedness.RightHanded:
					this.slash_button = this.RIGHT_SLASH_BUTTON;
					break;
				default:
					Debug.LogWarning("No OVRInput handedness found.");
					this.slash_button = this.DEFAULT_SLASH_BUTTON;
					break;
			}
		}
	}
}

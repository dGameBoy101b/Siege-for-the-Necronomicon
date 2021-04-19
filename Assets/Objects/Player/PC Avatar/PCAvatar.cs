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
	[Tooltip("The gauntlet to control with this player.")]
	public Gauntlet GAUNTLET;
	
	[SerializeField()]
	[Tooltip("The camera to control with this player.")]
	public Camera CAMERA;
	
	[Header("Controls")]

	[SerializeField()]
	[Tooltip("The input manager name for the button to use to activate the sword.")]
	public string SWORD_CONTROL;
	
	[SerializeField()]
	[Tooltip("The input manager name for the button to use to activate the gauntlet.")]
	public string GAUNTLET_CONTROL;
	
	[SerializeField()]
	[Tooltip("The input manager name for the axis that controls the horizontal camera rotation.")]
	public string LOOK_HORIZONTAL;
	
	[SerializeField()]
	[Tooltip("The input manager name for the axis that controls the vertical camera rotation.")]
	public string LOOK_VERTICAL;
	
	[SerializeField()]
	[Tooltip("A sensitivity multiplier for camera rotation.")]
	[Min(float.Epsilon)]
	public float LOOK_SENSITIVITY;
	
	[Header("Constraints")]
	
	[SerializeField()]
	[Tooltip("The maximum distance in units the sword and gauntlet can move away from the player.")]
	[Min(0)]
	public float EXTENT;
	
	/**
	 * The latitude in degrees of the player camera's rotation.
	 */
	private float look_lat;
	
	/**
	 * The longitude in degree of the player camera's rotation.
	 */
	private float look_lon;
	
	/**
	 * Return the projectile that is closest to this player from the given list.
	 * @param projs The list of projectiles to search through.
	 * @return The closest projectile, of null if an empty list was given.
	 */
	private ProjectileBase closest(ProjectileBase[] projs)
	{
		if (projs.Length < 1)
		{
			return null;
		}
		ProjectileBase close = projs[0];
		float dist = Vector3.Distance(this.transform.position, close.transform.position);
		foreach (ProjectileBase proj in projs)
		{
			if (Vector3.Distance(this.transform.position, proj.transform.position) < dist)
			{
				close = proj;
				dist = Vector3.Distance(this.transform.position, close.transform.position);
			}
		}
		return close;
	}
	
	/**
	 * Extend an equipment towards a target.
	 * @param equip The equipment to move and rotate.
	 * @param targ The world position to face the equpiment to.
	 */
	private void extendEquipment(EquipmentBase equip, Vector3 targ)
	{
		Vector3 vec = (targ - this.transform.position).normalized * this.EXTENT;
		equip.transform.position = vec + this.transform.position;
		equip.transform.rotation = Quaternion.LookRotation(vec);
	}
	
	/**
	 * Swing the player's sword at the closest physical projectile.
	 * @return The physical projectile the was swung at, or null if no valid target was found.
	 */
	private PhysicalProjectileBase swingSword()
	{
		PhysicalProjectileBase targ = (PhysicalProjectileBase)this.closest(Object.FindObjectsOfType<PhysicalProjectileBase>());
		if (targ == null)
		{
			return null;
		}
		this.extendEquipment(this.SWORD, targ.transform.position);
		this.SWORD.createSlash(this.transform.position + Vector3.up, this.transform.position + Vector3.down);
		return targ;
	}
	
	/**
	 * Raise the player's gauntlet to block the closest magical projectile.
	 */
	private MagicalProjectileBase raiseGauntlet()
	{
		MagicalProjectileBase targ = (MagicalProjectileBase)this.closest(Object.FindObjectsOfType<MagicalProjectileBase>());
		if (targ == null)
		{
			return null;
		}
		this.extendEquipment(this.GAUNTLET, targ.transform.position);
		return targ;
	}
	
	/**
	 * Rotate the player's camera based on the look axes input.
	 */
	private void rotateCamera()
	{
		this.look_lat += Input.GetAxis(this.LOOK_VERTICAL) * this.LOOK_SENSITIVITY;
		this.look_lon += Input.GetAxis(this.LOOK_HORIZONTAL) * this.LOOK_SENSITIVITY;
		this.CAMERA.transform.localRotation = Quaternion.AngleAxis(this.look_lon, Vector3.up)
			* Quaternion.AngleAxis(this.look_lat, Vector3.left);
	}
	
	private void Update()
	{
		if (Input.GetButtonDown(this.SWORD_CONTROL))
		{
			this.swingSword();
		}
		if (Input.GetButtonDown(this.GAUNTLET_CONTROL))
		{
			this.raiseGauntlet();
		}
		this.rotateCamera();
	}
	
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		this.look_lat = 0;
		this.look_lon = 0;
	}
}

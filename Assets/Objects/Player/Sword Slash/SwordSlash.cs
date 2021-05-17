 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class SwordSlash : MonoBehaviour
{
	/**
	 * The collision layer names that sword slashes should be destroyed against.
	 */
	public static string[] DESTROY_LAYERS {get;} = {"Level Bounds", "Physical Projectile"};
	
	[SerializeField()]
	[Tooltip("The speed in units per second this sword slash moves forward.")]
	public float SPEED;
	
	/**
	 * Test if a sword slash should be destroyed when it hits the given collider.
	 * @param col The colider to test.
	 * @return True if a sword slash should be destroyed against the given collider, false otherwise.
	 */
	public static bool shouldDestroy(Collider col)
	{
		return (LayerMask.GetMask(SwordSlash.DESTROY_LAYERS) 
			& LayerMask.GetMask(LayerMask.LayerToName(col.gameObject.layer))) != 0;
	}
	
	/**
	 * Move this sword slash forward one step.
	 * @param t The float number of seconds since the last call to this function.
	 */
	public void moveForward(float t)
	{
		this.transform.Translate(Vector3.forward * SPEED * t, Space.Self);
	}
	
	private void Update()
	{
		this.moveForward(Time.deltaTime);
	}
	
	private void OnTriggerEnter(Collider col)
	{
		if (SwordSlash.shouldDestroy(col))
		{
			Object.Destroy(this.gameObject);
			Debug.Log("destroyed by " + col.gameObject.name);
		}
	}
}

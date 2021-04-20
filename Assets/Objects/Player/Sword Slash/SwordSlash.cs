using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
	[SerializeField()]
	[Tooltip("The speed in units per second this sword slash moves forward.")]
	public float SPEED;
	
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyShield : MonoBehaviour
{
	[Tooltip("How many seconds this shield stays active.")]
	[Min(0)]
	public float emergencyShieldLength;

	/**
	 * The time (in seconds) at which this shield should deactivate.
	 */
	private float offTime;

	/**
	 * Deactivate this emergency shield.
	 */
	public void DeactivateInvincibility()
	{
		this.gameObject.SetActive(false);
	}

	/**
	 * Activate this emergency shield.
	 */
	public void ActivateInvincibility()
	{
		this.gameObject.SetActive(true);
		this.offTime = Time.time + emergencyShieldLength;
	}

	void Start()
	{
		this.gameObject.SetActive(false);
	}

	void Update()
	{
		if(Time.time > this.offTime)
		{
			this.DeactivateInvincibility();
		}
	}
}

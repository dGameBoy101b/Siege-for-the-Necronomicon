using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyShield : MonoBehaviour
{
	public Health PLAYER_HEALTH;

	public float emergencyShieldLength;

	private float offTime;

	public void DeactivateInvincibility()
	{
		this.gameObject.SetActive(false);
	}

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

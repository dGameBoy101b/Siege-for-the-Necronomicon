using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeButton : MonoBehaviour
{
	[SerializeField()]
	[Tooltip("The menu this resume button should hide and release control from when clicked.")]
	public Canvas CUR_MENU;
	
	[SerializeField()]
	[Tooltip("The pause script this resume button should send an unpause message to.")]
	public Pause PAUSE;
	
	/**
	 * Resume the game.
	 */
	public void resumeGame()
	{
		Debug.Log("resume");
		this.PAUSE.unpause();
		this.CUR_MENU.gameObject.SetActive(false);
	}
}

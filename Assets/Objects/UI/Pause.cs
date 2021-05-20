using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 24/04/2021
 * A class to trigger pausing when the pause button is pressed.
 */
public class Pause : MonoBehaviour
{
	[SerializeField()]
	[Tooltip("The name of the input manager button used to open the pause menu.")]
	public string PAUSE_BUTTON;
	
	[SerializeField()]
	[Tooltip("The canvas to show when the pause button is pressed.")]
	public Canvas PAUSE_MENU;
	
	[SerializeField()]
	[Tooltip("The wave manager used in this level.")]
	public WaveManager WAVE_MANAGER;
	
	[SerializeField()]
	[Tooltip("The canvas to hide when the pause button is pressed.")]
	public Canvas HUD;
	
	[SerializeField()]
	[Tooltip("The pointer used in VR menus.")]
	public LaserPointer POINTER;
	
	/**
	 * Pause the game.
	 */
	public void pause()
	{
		Cursor.lockState = CursorLockMode.Confined;
		this.HUD.gameObject.SetActive(false);
		this.PAUSE_MENU.gameObject.SetActive(true);
		this.WAVE_MANAGER.pauseAttacks();
		this.POINTER.GetComponent<LineRenderer>().enabled = true;
	}
	
	/**
	 * Unpause the game.
	 */
	public void unpause()
	{
		Cursor.lockState = CursorLockMode.Locked;
		this.PAUSE_MENU.gameObject.SetActive(false);
		this.HUD.gameObject.SetActive(true);
		this.WAVE_MANAGER.unpauseAttacks();
		this.POINTER.GetComponent<LineRenderer>().enabled = false;
	}
	
	private void Update()
	{
		if (Input.GetButtonDown(this.PAUSE_BUTTON) || OVRInput.GetDown(OVRInput.Button.Start))
		{
			this.pause();
		}
	}
	
	private void Start()
	{
		Time.timeScale = 1;
	}
}

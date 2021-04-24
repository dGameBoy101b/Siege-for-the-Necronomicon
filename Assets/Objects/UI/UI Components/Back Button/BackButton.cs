using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BackButton : MonoBehaviour
{
	[Tooltip("The menu this back button should hide and release control from when clicked.")]
	public Canvas CUR_MENU;
	
	[Tooltip("The menu this back button should show and return control to when clicked.")]
	public Canvas RET_MENU;
	
	/**
	 * Hide this menu and return control to the previous menu.
	 */
	public void goBack()
	{
		this.CUR_MENU.gameObject.SetActive(false);
		this.RET_MENU.gameObject.SetActive(true);
	}
}

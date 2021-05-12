using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplay : MonoBehaviour
{
	[Tooltip("The text element ot display the value.")]
	public TMPro.TMP_Text VALUE_TEXT;
	
	/**
	 * The value to display.
	 */
	public System.Object Value {get; set;}
	
	/**
	 * Update the text display with the current value.
	 */
	public void updateText()
	{
		this.VALUE_TEXT.text = this.Value.ToString();
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionSlider : MonoBehaviour
{
	[Header("Object References")]
	
	[SerializeField()]
	[Tooltip("The slider that modifies the linked option.")]
	public Slider SLIDER;
	
	[SerializeField()]
	[Tooltip("The text element that displays the minimum value.")]
	public TMPro.TMP_Text MIN_TEXT;
	
	[SerializeField()]
	[Tooltip("The text element that displays the maximum value.")]
	public TMPro.TMP_Text MAX_TEXT;
	
	[SerializeField()]
	[Tooltip("The text element that displays the current value.")]
	public TMPro.TMP_Text VAL_TEXT;
	
	[Header("Option Settings")]
	
	[SerializeField()]
	[Tooltip("The name of the linked option.")]
	public string NAME;
	
	[SerializeField()]
	[Tooltip("The minimum value.")]
	public float MIN;
	
	[SerializeField()]
	[Tooltip("The maximum value.")]
	public float MAX;
	
	[SerializeField()]
	[Tooltip("The default value.")]
	public float DEFAULT;
	
	[Header("Display Settings")]
	
	[SerializeField()]
	[Tooltip("The number of decimal places that should be displayed.")]
	[Range(0,5)]
	public int PRECISION;
	
	[SerializeField()]
	[Tooltip("Should text displays always have a plus sign if the value is psoitive?")]
	public bool FORCE_PLUS;
	
	/**
	 * Check to ensure the minimum is lesser than the maximum.
	 * @throws Exception The minimum is greater than or equal to the maximum.
	 */
	private void checkRange()
	{
		if (this.MIN >= this.MAX)
		{
			throw new Exception("The minimum is greater than the maximum!");
		}
	}
	
	/**
	 * Update the minimum and maximum displays and the slider value bounds.
	 */
	private void updateRange()
	{
		this.MIN_TEXT.text = this.ensurePlus(this.ensurePrecision(this.MIN.ToString()));
		this.MAX_TEXT.text = this.ensurePlus(this.ensurePrecision(this.MAX.ToString()));
		this.SLIDER.minValue = this.MIN;
		this.SLIDER.maxValue = this.MAX;
		Debug.Log(this.gameObject.name + ": range update");
	}
	
	/**
	 * Set the value of this slider to that of the linked option or the default if not found.
	 */
	private void fetchValue()
	{
		if (!OptionStore.Instance.hasOption(this.NAME))
		{
			OptionStore.Instance.addOption(this.NAME, this.DEFAULT);
		}
		this.SLIDER.value = (float)OptionStore.Instance.getOption(this.NAME);
		this.VAL_TEXT.text = this.ensurePlus(this.ensurePrecision(this.SLIDER.value.ToString()));
	}
	
	/**
	 * Update the value display and the linked option with the value of the linked slider.
	 */
	public void updateValue()
	{
		if (!OptionStore.Instance.hasOption(this.NAME))
		{
			OptionStore.Instance.addOption(this.NAME, this.DEFAULT);
		}
		OptionStore.Instance.setOption(this.NAME, this.SLIDER.value);
		this.VAL_TEXT.text = this.ensurePlus(this.ensurePrecision(this.SLIDER.value.ToString()));
	}
	
	/**
	 * Trim or pad the given number string to comply with the precision setting of this option slider.
	 * @param str The number string to process.
	 * @return The given string trimmed and padded to the specified precision.
	 */
	private string ensurePrecision(string str)
	{
		int dec_i = str.Length;
		if (str.Contains("."))
		{
			dec_i = str.IndexOf('.');
		}
		else
		{
			str += '.';
		}
		if (this.PRECISION == 0)
		{
			return str.Substring(0, dec_i);
		}
		dec_i += 1;
		while (str.Length - dec_i < this.PRECISION)
		{
			str += '0';
		}
		return str.Substring(0, dec_i + this.PRECISION);
	}
	
	/**
	 * Ensure the given number string has a plus sign if it doesn't have a negative sign.
	 * @param str The number string to midify.
	 * @return The given number string with a plus sign prepended if applicable.
	 */
	private string ensurePlus(string str)
	{
		if (this.FORCE_PLUS && !str.Contains("-"))
		{
			str = '+' + str;
		}
		return str;
	}
	
	private void Awake()
	{
		this.checkRange();
		this.updateRange();
		this.fetchValue();
	}
}

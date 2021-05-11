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
	public Text MIN_TEXT;
	
	[SerializeField()]
	[Tooltip("The text element that displays the maximum value.")]
	public Text MAX_TEXT;
	
	[SerializeField()]
	[Tooltip("The text element that displays the current value.")]
	public Text VAL_TEXT;
	
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
		this.MIN_TEXT.text = this.MIN.ToString();
		this.MAX_TEXT.text = this.MAX.ToString();
		this.SLIDER.minValue = this.MIN;
		this.SLIDER.maxValue = this.MAX;
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
		this.VAL_TEXT.text = this.SLIDER.value.ToString();
	}
	
	/**
	 * Update the value display and the linked option with the value of the linked slider.
	 */
	public void updateValue()
	{
		OptionStore.Instance.setOption(this.NAME, this.SLIDER.value);
		this.VAL_TEXT.text = this.SLIDER.value.ToString();
	}
	
	private void Awake()
	{
		this.checkRange();
		this.updateRange();
		this.fetchValue();
	}
}

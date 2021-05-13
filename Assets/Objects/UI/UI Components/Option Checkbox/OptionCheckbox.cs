using UnityEngine;
using UnityEngine.UI;

/**
 * @author Rhys Mader 33705134
 * @date 13/05/2021
 * A checkbox that links to the option store.
 */
public sealed class OptionCheckbox : MonoBehaviour
{
	[Header("Object References")]
	
	[SerializeField()]
	[Tooltip("The checkbox this is linked to.")]
	public Toggle CHECKBOX;
	
	[Header("Option Settings")]
	
	[SerializeField()]
	[Tooltip("The name of the linked option.")]
	public string NAME;
	
	[SerializeField()]
	[Tooltip("The default value of the linked option.")]
	public bool DEFAULT;
	
	/**
	 * Set the value of this checkbox to that of the linked option or the default if not found.
	 */
	private void fetchValue()
	{
		if (!OptionStore.Instance.hasOption(this.NAME))
		{
			OptionStore.Instance.addOption(this.NAME, this.DEFAULT);
		}
		this.CHECKBOX.isOn = (bool)OptionStore.Instance.getOption(this.NAME);
	}
	
	/**
	 * Update the linked option with the value of the linked checkbox.
	 */
	public void updateValue()
	{
		if (!OptionStore.Instance.hasOption(this.NAME))
		{
			OptionStore.Instance.addOption(this.NAME, this.DEFAULT);
		}
		OptionStore.Instance.setOption(this.NAME, this.CHECKBOX.isOn);
	}
	
	private void Awake()
	{
		this.fetchValue();
	}
}

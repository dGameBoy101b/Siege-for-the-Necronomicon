using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 13/05/2021
 * A small utility class to quickly save the current state of the option store.
 */
public sealed class SaveOptions : MonoBehaviour
{
	/**
	 * Save the current instance of an option store.
	 */
	public void saveOptions()
	{
		OptionStore.Instance.save();
	}
}

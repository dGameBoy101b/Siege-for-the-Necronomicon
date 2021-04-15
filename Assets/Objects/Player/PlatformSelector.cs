using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Rhys Mader 33705134
 * @date 15/04/2021
 * A class to conditionally enable game objects based on the runtime platform.
 */
public class PlatformSelector : MonoBehaviour
{
	[SerializeField()]
	[Tooltip("The platforms this object should be enabled for.")]
	public List<RuntimePlatform> PLATFORMS;
	
	/**
	 * Check if this game object should be active.
	 */
	private void checkPlatform()
	{
		this.gameObject.SetActive(this.PLATFORMS.Contains(Application.platform));
	}
	
	private void Awake()
	{
		this.checkPlatform();
	}
}

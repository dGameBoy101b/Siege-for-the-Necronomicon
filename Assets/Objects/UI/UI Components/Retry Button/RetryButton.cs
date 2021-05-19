using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
	/*[Tooltip("The build index of the scene to load when this button is clicked.")]
	public int LOAD_SCENE;*/
	
	/**
	 * reloads the linked scene.
	 */
	public void retryScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

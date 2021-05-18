using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
	/*[Tooltip("The build index of the scene to load when this button is clicked.")]
	public int LOAD_SCENE;*/
	
	/**
	 * Load the linked scene.
	 */
	public void loadScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

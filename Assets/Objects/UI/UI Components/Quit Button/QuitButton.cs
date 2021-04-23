using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class QuitButton : MonoBehaviour
{
	/**
	 * Quit the game.
	 */
	public void quit()
	{
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
		#else
			Application.Quit();
		#endif
	}
}

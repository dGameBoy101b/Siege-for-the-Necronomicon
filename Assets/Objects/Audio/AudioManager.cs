using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* @author Allan Zheng 33690777
* @date 18/04/2021
* The class for managing audio in a scene. 
*/
public class AudioManager : MonoBehaviour
{
	[Tooltip("To keep the current soundtrack playing seamlessly when moving to the next scene.")]
	public bool StayActive;

	[Tooltip("The different audio sources for the AudioManager to hold.")]
	public AudioSource[] sounds;

	public static AudioManager instance;

	/*
	* Will check if the gameobject which is the AudioManager will need to stay active or not. 
	*/
	void Awake()
	{
		if (StayActive == true)
		{
			if (instance == null) 
			{
				instance = this;
			}
			else
			{
				Destroy(gameObject);
				return;
			}
			DontDestroyOnLoad(gameObject); 
		}
	}

	/*
	* Will play the theme titled "Start Theme" in the AudioManager.
	*/
	void Start()
	{
		this.sounds[0].Play();
	}
}

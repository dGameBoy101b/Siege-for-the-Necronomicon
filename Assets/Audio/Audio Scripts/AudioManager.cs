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

    [Tooltip("The amount of different soundtracks for the AudioManager to hold.")]
    public Sound[] sounds;

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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

    }

    /*
    * Will play the theme titled "Start Theme" in the AudioManager.
    */
    void Start()
    {
        Play("Start Theme");
    }

    /*
    * Will start playing what ever audio track is string name in the AudioManager
    * @parem string name - represents the name of the soundtrack in the AudioManager
    */
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
 
        s.source.Play();
        Debug.Log("Soundtrack " + name + " playing!");
    }

    /*
    * Will stop playing what ever audio track is string name in the AudioManager
    * @parem string name - represents the name of the soundtrack in the AudioManager
    */
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Stop();
        Debug.Log("Soundtrack " + name + " stopping!");
    }

    /*
    * Will pause playing what ever audio track is string name in the AudioManager
    * @parem string name - represents the name of the soundtrack in the AudioManager
    */
    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }

        s.source.Pause();
        Debug.Log("Soundtrack " + name + " paused!");
    }
}

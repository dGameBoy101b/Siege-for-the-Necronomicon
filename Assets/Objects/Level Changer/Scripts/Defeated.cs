using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 18/05/2021
 * Handles the transition upon defeating the Necronomicon
 */
public class Defeated : MonoBehaviour
{
    [SerializeField()]
    [Tooltip("The open book asset under the Necronomicon in the Transition Manager")]
    public GameObject BOOK;

    [SerializeField()]
    [Tooltip("The environment around the Necronomicon.")]
    public GameObject NECRONOMICON_ENVIRONMENT;

    [SerializeField()]
    [Tooltip("The environment that is present when the world starts.")]
    public GameObject BROKEN_WORLD;

    [SerializeField()]
    [Tooltip("The environment that replaces the broken world after the Necronomicon is defeated.")]
    public GameObject FIXED_WORLD;

    [SerializeField()]
    [Tooltip("The level changer prefab.")]
    public LevelChanger LEVEL_CHANGER;

    [SerializeField()]
    [Tooltip("The delay after the flash and before the scene changes.")]
    public float DELAY_BEFORE_SCENE_CHANGE;

    [SerializeField()]
    [Tooltip("The the weather after the flash")]
    public int ENVIRO_PRESET_INDEX;

    [SerializeField()]
    [Tooltip("The the TOD after the flash")]
    public int TOD;

    [SerializeField()]
    [Tooltip("All the audio that plays when the Necronomicon is defeated.")]
    public AudioSource[] DEFEATED_SOUNDS;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            defeatNeronomicon();
        }
    }

    /*
    * Will trigger the various steps to complete the transition of defeating the Necronomicon
    */
    public void defeatNeronomicon()
    {
        Destroy(BOOK);
        Destroy(NECRONOMICON_ENVIRONMENT);
        GetComponent<Animator>().Play("Flash");
        playDefeatedSounds();
        Invoke("changeWorld", 2);
        Invoke("levelCompleted", DELAY_BEFORE_SCENE_CHANGE);
    }

    /*
    * Will remove the broken world and replace with the Fixed world.
    */
    public void changeWorld()
    {
        EnviroSkyMgr.instance.SetTimeOfDay(TOD);
        EnviroSkyMgr.instance.ChangeWeatherInstant(ENVIRO_PRESET_INDEX);
        Destroy(BROKEN_WORLD);
        FIXED_WORLD.SetActive(true);
    }

    /*
    * Will use LevelChanger.cs to fade out to the next level.
    */
    public void levelCompleted()
    {
        LEVEL_CHANGER.nextLevel();
    }

    /*
    * Will play all the audio for defeating the Necronomicon
    */
    protected void playDefeatedSounds()
    {
        foreach (AudioSource aud in this.DEFEATED_SOUNDS)
        {
            aud.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void defeatNeronomicon()
    {
        Destroy(BOOK);
        Destroy(NECRONOMICON_ENVIRONMENT);
        GetComponent<Animator>().Play("Flash");
        playDefeatedSounds();
        Invoke("changeWorld", 2);
        Invoke("levelCompleted", DELAY_BEFORE_SCENE_CHANGE);
    }

    public void deleteBook()
    {
        Destroy(BOOK);
    }

    public void changeWorld()
    {
        EnviroSkyMgr.instance.SetTimeOfDay(TOD);
        EnviroSkyMgr.instance.ChangeWeatherInstant(ENVIRO_PRESET_INDEX);
        BROKEN_WORLD.SetActive(false);
        FIXED_WORLD.SetActive(true);
    }

    public void levelCompleted()
    {
        LEVEL_CHANGER.nextLevel();
    }

    protected void playDefeatedSounds()
    {
        foreach (AudioSource aud in this.DEFEATED_SOUNDS)
        {
            aud.Play();
        }
    }
}

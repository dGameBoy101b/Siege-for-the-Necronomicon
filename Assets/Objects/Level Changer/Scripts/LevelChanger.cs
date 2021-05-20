using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 12/05/2021
 * Handles the scene fade in and fade out animations.
 */

public class LevelChanger : MonoBehaviour
{
    [SerializeField()]
    [Tooltip("The animator that contains the fade in and fade out animations.")]
    public Animator animator;

    [SerializeField()]
    [Tooltip("The index of the level that the scene fader should load to.")]
    public int LevelToLoad;

    [SerializeField()]
    [Tooltip("If true, levelToLoad will be ignored and LevelChanger will head to the next index / level.")]
    public bool goToNextLevelInstead;

    [SerializeField()]
    [Tooltip("The barrier shatter script.")]
    public shatter BARRIER;

    [SerializeField()]
    [Tooltip("The amount of time before the next level loads.")]
    public float DelayToNextLevel;

    [Header("Cinematic Mode Components.")]

    [SerializeField()]
    [Tooltip("If true, enables the cinematic mode where the above components are irrelevant. Instead the scene will be active for intro time length before heading to the next scene")]
    public bool cinematicMode;

    [SerializeField()]
    [Tooltip("Tick this if the scene is the outro scene / credit scroll. Also set / 'Level To Load' to index of the Main Menu or the scene that is loaded after the outro.")]
    public bool isOutro;

    [SerializeField()]
    [Tooltip("The amount of time the scene plays for.")]
    public float lifeSpanTime;

    void Start()
    {
        if(cinematicMode == true)
        {
            if (isOutro == true)
            {
                Invoke("loadAfterOutro", lifeSpanTime);
            }
            else
            {
                Invoke("nextLevel", lifeSpanTime);
            }
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("PC Gauntlet") && goToNextLevelInstead)
        {
            BARRIER.shatterBarrier();
            Debug.Log("Load next level");
            Invoke("nextLevel", DelayToNextLevel);
        }
        else
            if(Input.GetButtonDown("PC Gauntlet"))
        {
                BARRIER.shatterBarrier();
                Debug.Log("Loading level " + LevelToLoad);
                FadeToLevel(LevelToLoad);
            }
    }
    /*
     * Is the trigger for the end barrier.
     */
    public void progress()
    {
        BARRIER.shatterBarrier();
        if (goToNextLevelInstead == true)
        {
            Debug.Log("Load next level");
            Invoke("nextLevel", DelayToNextLevel); 
        }
        else
        {
            Debug.Log("Loading level " + LevelToLoad);
            FadeToLevel(LevelToLoad);
        }
    }

    /*
     * Will load next level.
     */
    public void nextLevel()
    {
        Debug.Log("Fading to next area...");
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /*
     * Fades out and loads the desired level.
     */
    public void FadeToLevel(int levelIndex)
    {
        LevelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    /*
     * When the fade out is complete, the next level will load. 
     */
    public void OnFadeComplete()
    {
        Debug.Log("OnFadeComplete Called");
        SceneManager.LoadScene(LevelToLoad);
    }

    /*
     * After the outro life span, the level to load index will load the corresponding scene.
     */
    public void loadAfterOutro()
    {
        FadeToLevel(LevelToLoad);
    }
}

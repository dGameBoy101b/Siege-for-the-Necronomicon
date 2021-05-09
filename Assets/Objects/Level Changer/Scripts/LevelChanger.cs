using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelChanger : MonoBehaviour
{
    public Animator animator;

    public int LevelToLoad;

    public bool goToNextLevel;

    public shatter SHAT;

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.H)) && goToNextLevel == true)
        {
            SHAT.shatterBarrier();
            Invoke("nextLevel", 8);
        }
        else
            if((Input.GetKeyDown(KeyCode.H)))
            {
                FadeToLevel(LevelToLoad);
            }
    }

    public void nextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        LevelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(LevelToLoad);
    }


}

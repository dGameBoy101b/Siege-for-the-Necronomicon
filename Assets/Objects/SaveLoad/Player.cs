using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int[] health;
    public int[] highScore;
    public bool[] completed; 
    public float[] timeLeft;
    public string[] scenes;
    public string currentscene;
    public int size;
    public int j;
    
    // Start is called before the first frame update
    void Awake()
    {
        ReadScenes();
        currentscene = SceneManager.GetActiveScene().name;
        Debug.Log(currentscene);

        health = new int[size];
        highScore = new int[size];
        completed = new bool[size];
        timeLeft = new float[size];

        for(int i = 0; i < size; i++)
        {
            health[i] = 3;
            highScore[i] = 0;
            completed[i] = false;
            timeLeft[i] = 10f;
        }

        this.LoadPlayer();

        for(int i = 0; i < size; i++)
        {
            if(scenes[i] == currentscene)
            {
                j = i;
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 
        health[j] = GameObject.FindObjectOfType<Health>().currentHealth;
        timeLeft[j] = GameObject.FindObjectOfType<UITimer>().timeRemaining;
        if(timeLeft[j] == 0)
        {
            completed[j] = true;
        }
        highScore[j] = GameObject.FindObjectOfType<ScoreSystem>().currentScore;
    }

    /**
    *this reads the scenes in the game build and stores them as an array so that the save data for each can be found
    */
    private void ReadScenes()
    {
        size = SceneManager.sceneCountInBuildSettings;
        Debug.Log(size);
        scenes = new string[size];

        for(int i = 0; i < size; i++)
        {
            string pathToScene = SceneUtility.GetScenePathByBuildIndex(i);
            scenes[i] = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
            Debug.Log(System.IO.Path.GetFileNameWithoutExtension(pathToScene));
        }
    }

    public int FindIndex(string levelName)
    {
        for(int i = 0; i < size; i++)
        {
            if(scenes[i] == levelName)
            {
                return(i);
            }
        }

        Debug.LogError("level doesnt exist");
        return(-1);
    }

    public void SavePlayer()
    {
        SaveLoad.SaveData(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveLoad.LoadData();

        for(int i = 0; i < size; i++)
        {
            health[i] = data.health[i];
            highScore[i] = data.highScore[i];
            completed[i] = data.completed[i];
            timeLeft[i] = data.timeLeft[i];
        }
    }
}

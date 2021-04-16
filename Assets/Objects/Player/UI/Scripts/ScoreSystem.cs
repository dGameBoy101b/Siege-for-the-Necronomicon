using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{

    public Text currentScoreLabel;
    public int currentScore;


    void Start()
    {
        currentScore = 0;
        
    }

    private void ScoreUpdate()
    {
        currentScoreLabel.text = currentScore.ToString();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Physical")
        {
            currentScore += 10;
            ScoreUpdate();
        }

        if (col.tag == "Magic")
        {
            currentScore += 20;
            ScoreUpdate();
        }
    }

    void Update()
    {
        
    }
}

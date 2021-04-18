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
        ScoreUpdate();
        
    }

    private void ScoreUpdate()
    {
        currentScoreLabel.text = currentScore.ToString();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Projectile")
        {
            currentScore += 10;
            ScoreUpdate();
            Destroy(col.gameObject);

        }

    }

}

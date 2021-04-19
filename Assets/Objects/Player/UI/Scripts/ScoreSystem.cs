using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{


    public Text currentScoreLabel;
    public Text endScoreLabel;
    public Text endTimeScoreLabel;

    [HideInInspector]
    public int currentScore;
    [HideInInspector]
    public int endScore;
    


    void Start()
    {

        currentScore = 0;
        endScore = currentScore;
        ScoreUpdate();
        
    }

    void ScoreUpdate()
    {
        currentScoreLabel.text = currentScore.ToString();
        endScoreLabel.text = currentScore.ToString();
        endTimeScoreLabel.text = currentScore.ToString();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 12/05/2021
 * Is the trigger for the end barrier.
 */
public class EndTrigger : MonoBehaviour
{
    [SerializeField()]
    [Tooltip("The Level Changer")]
    public LevelChanger LEVEL_CHANGER;

    [SerializeField()]
    [Tooltip("The End Barrier")]
    public GameObject ENDING_BARRIER;

    /*
     * Is the trigger for the end barrier.
     */
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("End Trigger Activated");
        Destroy(ENDING_BARRIER);
        LEVEL_CHANGER.progress();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 18/05/2021
 * Triggers the defeat of the Necronomicon
 */
public class DefeatTrigger : MonoBehaviour
{
    public Defeated DEFEATED_SCRIPT;

    /*
    * Will trigger the defeated tranisiton upon the player touching the trigger. 
    */
    void OnTriggerEnter(Collider other)
    {
        DEFEATED_SCRIPT.defeatNeronomicon();
    }
}

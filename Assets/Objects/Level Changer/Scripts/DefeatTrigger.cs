using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatTrigger : MonoBehaviour
{
    public Defeated DEFEATED_SCRIPT;

    void OnTriggerEnter(Collider other)
    {
        DEFEATED_SCRIPT.defeatNeronomicon();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 12/05/2021
 * Is the trigger for the Starting Orb
 */

public class BeginTrigger : MonoBehaviour
{
    [SerializeField()]
    [Tooltip("The Transition Manager")]
    public Transition TRANSITION_MANAGER;

    [SerializeField()]
    [Tooltip("The Starting Orb / Trigger the player touches to begin.")]
    public GameObject STARTING_ORB;

    private bool hasBegun = false;

    /*
     * Is the trigger of the starting orb.
     */
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            Destroy(STARTING_ORB);
            TRANSITION_MANAGER.begin();
        }
    }

    /*
    *   trigger for the starting orb on pc
    */
    void Update()
	{
		if (Input.GetAxis("PC Gauntlet") > 0 && hasBegun == false)
		{
            Destroy(STARTING_ORB);
			TRANSITION_MANAGER.begin();
			hasBegun = true;
		}
	}
}

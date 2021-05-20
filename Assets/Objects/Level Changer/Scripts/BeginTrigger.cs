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

    [SerializeField()]
    [Tooltip("The name of the collision layer that needs to collide with this to begin.")]
    public string START_LAYER;

    private bool hasBegun = false;

    /*
     * Is the trigger of the starting orb.
     */
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(this.START_LAYER))
        {
            this.begin();
        }
    }

    /*
    *   trigger for the starting orb on pc
    */
    void Update()
	{
		if (Input.GetButtonDown("PC Gauntlet"))
		{
            this.begin();
		}
	}

    private void begin()
    {
        if (!this.hasBegun)
		{
            this.TRANSITION_MANAGER.begin();
            GameObject.Destroy(STARTING_ORB);
            this.hasBegun = true;
        }
    }
}

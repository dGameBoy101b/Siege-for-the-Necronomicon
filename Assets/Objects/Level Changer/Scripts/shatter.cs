using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Allan Zheng 33690777
 * @date 12/05/2021
 * Handles the shattering of the end level barrier.
 */

public class shatter : MonoBehaviour
{
    [SerializeField()]
    [Tooltip("The shattered variant of the end game Barrier")]
    public GameObject SHATTERED;

    [SerializeField()]
    [Tooltip("All of the audio for when the barrier shatters")]
    public AudioSource[] BARRIER_SHATTER;

    [SerializeField()]
    [Tooltip("The amount of power the barrier shatters with. (The higher the stronger the shatter)")]
    public float BREAK_FORCE;

    /*
     * Shatters the end game barrier.
     */
    public void shatterBarrier()
    {
        playShatterFX();
        GameObject frac = Instantiate(SHATTERED, transform.position, transform.rotation);

        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * BREAK_FORCE;
            rb.AddForce(force);
        }

        Destroy(gameObject);
    }

    /*
     * Plays all the audio associated with the shattering of the endgame barrier.
     */
    protected void playShatterFX()
    {
        foreach (AudioSource aud in this.BARRIER_SHATTER)
        {
            aud.Play();
        }
    }


}

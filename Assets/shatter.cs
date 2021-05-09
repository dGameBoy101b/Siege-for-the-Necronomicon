using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shatter : MonoBehaviour
{
    public GameObject SHATTERED;

    public float BREAK_FORCE;

    public void shatterBarrier()
    {
        GameObject frac = Instantiate(SHATTERED, transform.position, transform.rotation);

        foreach (Rigidbody rb in frac.GetComponentsInChildren<Rigidbody>())
        {
            Vector3 force = (rb.transform.position - transform.position).normalized * BREAK_FORCE;
            rb.AddForce(force);
        }

        Destroy(gameObject);
    }


}

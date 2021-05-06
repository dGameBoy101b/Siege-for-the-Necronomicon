using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
*  @author Connor Burnside 33394927
*@date 3/05/2021
* a script for restarting the credit animation
*/
public class Credits : MonoBehaviour
{   
    //the animation component so we can start and reset animation
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable() 
    {
        anim.Play("Credits Animation", 0, 0f);    
    }
}

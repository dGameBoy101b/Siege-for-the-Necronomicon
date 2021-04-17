using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
*@author Connor Burnside 33394927   
*@date 8/04/2021
* a gaunlet object that collides with magical projectiles, destroying them and adding to the score, inherits from the EquipmentBase
*/
public class Gauntlet : EquipmentBase
{
    //destroy the projectile on collision
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.layer == 8)  
        {
            Destroy(other.gameObject);
            Debug.Log("blocked magic projectile");
        }  
    }
}

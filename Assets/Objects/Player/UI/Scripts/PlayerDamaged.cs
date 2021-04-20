using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @author Kai Sweeting 33268787
 * @date 16/04/2021
 * Script that enables projectiles to damage the player's health on collision
 */

public class PlayerDamaged : MonoBehaviour
{
	    public GameObject player;
		public int damage;
		Health health;


	    void Start()
	    {
	        player = GameObject.FindWithTag("Player");
			health = player.GetComponent<Health>();
	    }

		/** @param col The projectile collider that damages player health by 1 point on collision
		 * on collision with player object, the projectile is destroyed
		 */
		void OnTriggerEnter(Collider col)
		{	
			if(col.gameObject.tag == "Player")
			{
				Attack();
				Destroy(this.gameObject);
			}
			
		}

		/** checks player current health value on projectile collision
		 * if it is above 0 player takes damage
		 */
	    void Attack()
	    {
	        if (health.currentHealth > 0)
	        {
	            health.TakeDamage(damage);
				health.UpdateGUI();
	        }
			
	    }
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

		void Update()
		{
			//if(health.currentHealth == 0)
			//{
				health.PlayerDead();
			//}
		}

		void OnTriggerEnter(Collider col)
		{
			if(col.gameObject.tag == "Player")
			{
				Attack();
				Destroy(this.gameObject);
			}
			
		}

	    void Attack()
	    {
	        if (health.currentHealth > 0)
	        {
	            health.TakeDamage(damage);
				health.UpdateGUI();
	        }
			
	    }
	
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int maxHealth;
    public Text currentHealthLabel;
    public int currentHealth;
    
    void Start()
    {
        currentHealth = maxHealth;
        UpdateGUI();
        
    }

    void UpdateGUI()
    {
        currentHealthLabel.text = currentHealth.ToString();
         if ( currentHealth == 0)
        {
            Debug.Log("Game Over!");
        }
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Projectile"){
            currentHealth -= 1;
            Debug.Log("You have been hit!");
        }
    }

    public void UpdateHealth(int damage)
    {
        currentHealth += damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateGUI();
    }
}

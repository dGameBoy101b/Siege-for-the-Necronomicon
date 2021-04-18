using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int maxHealth;
    public Text currentHealthLabel;
    public int currentHealth;

    
    void Awake()
    {
        currentHealth = maxHealth;
        UpdateGUI();
        
    }

    public void UpdateGUI()
    {
        currentHealthLabel.text = currentHealth.ToString();
        
         if (currentHealth == 0)
        {
            Debug.Log("Game Over!");
        }
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateGUI();
    }
}

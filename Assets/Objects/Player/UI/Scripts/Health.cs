using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{

    public int maxHealth;
    public Text currentHealthLabel;
    public Image gameOver;

    [HideInInspector]
    public int currentHealth;
    [HideInInspector]
    public bool isDead;


    private GameObject projectile;


    [Tooltip("The game over screen will show on player death if this is enabled")]
    public bool gameOverScreen = true;


    
    void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        projectile = GameObject.FindWithTag("ProjectileSpawn");
        UpdateGUI();
        
    }

    public void UpdateGUI()
    {
        currentHealthLabel.text = currentHealth.ToString();
        gameOver.gameObject.SetActive(isDead);
        PlayerDead();  
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateGUI();
    }

    public void PlayerDead()
    {
            if(currentHealth == 0)
            {
                projectile.GetComponent<WaveManager>().enabled = false;
            }
            else
            if(gameOverScreen == true)
                {
                    if(currentHealth == 0)
                    {
                        isDead = true;
                        projectile.GetComponent<WaveManager>().enabled = false;
                    }
                }
                else
                {
                    return;   
                }
                
        
    }
}

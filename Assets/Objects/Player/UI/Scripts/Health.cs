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
    //[HideInInspector]
    private bool isDead;


    private GameObject projectile;

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
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        PlayerDead();
        UpdateGUI();
        
    }

    void PlayerDead()
    {
        if(currentHealth == 0)
            {
                isDead = true;
                projectile.GetComponent<WaveManager>().enabled = false;
                UpdateGUI();
            } 
        else
            return; 

    }
}


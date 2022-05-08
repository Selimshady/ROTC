using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHealth; 

    public int maxHealth;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public bool Damage(int damage)
    {
        currentHealth-=damage;
        if(currentHealth <= 0)
        {
            return true;
        }
        return false;
    }

    public void Heal(int itemHealth)
    {
        if(currentHealth + itemHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth +=itemHealth;
        }
    }

    public void Respawn()
    {
        currentHealth = maxHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public void setHealth(int health)
    {
        currentHealth = health;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}

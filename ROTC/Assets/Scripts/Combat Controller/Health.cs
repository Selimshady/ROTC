using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int currentHealth; 
    private int maxHealth;
    
    private void Start()
    {
        maxHealth = States.instance.getMaxHealth();
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

    /*public void Heal(int itemHealth)
    {
        if(currentHealth + itemHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth +=itemHealth;
        }
    }*/

    /*public void Respawn()
    {
        currentHealth = maxHealth;
    }*/

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    /*public void setHealth(int health)
    {
        currentHealth = health;
    }*/

    /*public void setMaxHealth(int health)
    {
        maxHealth = health;
    }*/

    /*public int getMaxHealth()
    {
        return maxHealth;
    }*/

    public void upgradeHealth()
    {
        maxHealth++;
        currentHealth = maxHealth;
        States.instance.setMaxHealth(maxHealth);
        Collection.instance.updateSkulls(-10);
    }


    ///////////Under Progress ////////////
    public void endDamage()
    {
        GetComponentInParent<EnemyMovement>().DamageEnd();
    }

    public void EndOfAttack()
    {
        GetComponentInParent<Enemy1Movement>().endOfAttack();
    }
}

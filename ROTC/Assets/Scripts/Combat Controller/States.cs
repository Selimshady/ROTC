using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{

    public static States instance;

    [Header("Health")]
    private int maxHealth; // done
    private int currentHealth; // done

    [Header("Level")]
    private int level;

    [Header("Player")]
    private float speed; // done
    private int Damage; // done
    private int skulls; // kaldı.

    [Header("Skill Cooldown")] 
    private float cooldown; // done

    private void Awake()
    {
        instance = this;
        loadData();
    }

    public void saveData()
    {
        PlayerPrefs.SetInt("maxHealth",maxHealth);
        PlayerPrefs.SetInt("currentHealth",currentHealth);
        PlayerPrefs.SetInt("Damage",Damage);
        PlayerPrefs.SetInt("level",level);
        PlayerPrefs.SetInt("skeletons",skulls);
        PlayerPrefs.SetFloat("speed",speed);
        PlayerPrefs.SetFloat("cooldwon",cooldown);
    }

    public void loadData()
    {
        maxHealth = PlayerPrefs.GetInt("maxHealth",2);
        currentHealth = PlayerPrefs.GetInt("currentHealth",2);
        Damage = PlayerPrefs.GetInt("Damage",1);
        level = PlayerPrefs.GetInt("level",1);
        skulls = PlayerPrefs.GetInt("skeletons",0);
        speed = PlayerPrefs.GetFloat("speed",5f);
        cooldown = PlayerPrefs.GetFloat("cooldown",4f);
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }

    public int getCurrentHealth()
    {
        return currentHealth;
    }

    public int getDamage()
    {
        return Damage;
    }

    public int getLevel()
    {
        return level;
    }

    public int getSkeletons()
    {
        return skulls;
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getCooldown()
    {
        return cooldown;
    }

    public void setMaxHealth(int health)
    {
        maxHealth = health;
    }

    public void setCurrentHealth(int health)
    {
        currentHealth = health;
    }

    public void setDamage(int damage)
    {
        Damage = damage;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }

    public void setSkeletons(int skulls)
    {
        this.skulls = skulls;
    }

    public void setSpeed(float speed)
    {
        this.speed = speed; 
    }

    public void setCooldown(float cooldown)
    {
        this.cooldown = cooldown;
    }
}

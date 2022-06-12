using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class States : MonoBehaviour
{

    public static States instance;

    [Header("Health")]
    private int maxHealth; 
    private int currentHealth; 

    [Header("Level")]
    private string level;

    [Header("Player")]
    private float speed; 
    private int Damage; 
    private int skulls; 

    [Header("Skill Cooldown")] 
    private float cooldown; 

    [Header("Enemy")]
    private int enemyDamage;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        instance = this;
        if(SceneManager.GetActiveScene().name.Equals("Main Level") || SceneManager.GetActiveScene().name.Equals("Level 1"))
            loadData();
    }

    private void Start()
    {
    }

    public void saveData()
    {
        PlayerPrefs.SetInt("maxHealth",maxHealth);
        PlayerPrefs.SetInt("currentHealth",currentHealth);
        PlayerPrefs.SetInt("Damage",Damage);
        PlayerPrefs.SetString("level",level);
        PlayerPrefs.SetInt("skulls",skulls);
        PlayerPrefs.SetFloat("speed",speed);
        PlayerPrefs.SetFloat("cooldwon",cooldown);
        PlayerPrefs.SetInt("enemyDamage",enemyDamage);
    }

    public void loadData()
    {
        maxHealth = PlayerPrefs.GetInt("maxHealth",5);
        currentHealth = PlayerPrefs.GetInt("currentHealth",5);
        Damage = PlayerPrefs.GetInt("Damage",1);
        level = PlayerPrefs.GetString("level","Level 1");
        skulls = PlayerPrefs.GetInt("skulls",10);
        speed = PlayerPrefs.GetFloat("speed",5f);
        cooldown = PlayerPrefs.GetFloat("cooldown",4f);
        enemyDamage = PlayerPrefs.GetInt("enemyDamage",1);
    }

    public bool CheckSaveFile() // For main menu
    {
        return PlayerPrefs.HasKey("level");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level 1");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("level"));
    }

    public int getEnemyDamage()
    {
        return enemyDamage;
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

    public string getLevel()
    {
        return level;
    }

    public int getSkulls()
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

    public void setEnemyDamage(int enemyDamage)
    {
        this.enemyDamage = enemyDamage;
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

    public void setLevel(string level)
    {
        this.level = level;
    }

    public void setSkulls(int skulls)
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

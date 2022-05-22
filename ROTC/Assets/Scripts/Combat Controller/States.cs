using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class States : MonoBehaviour
{
    [Header("Health")]
    private int maxHealth;
    private int currentHealth;

    [Header("Weapons")]
    private int swordDamage;
    private int arrowDamage;
    private int fireballDamage;

    [Header("Level")]
    private int level;

    [Header("Collectible")]
    private int skeletons;

    public void saveData()
    {
        PlayerPrefs.SetInt("maxHealth",maxHealth);
        PlayerPrefs.SetInt("currentHealth",currentHealth);
        PlayerPrefs.SetInt("swordDamage",swordDamage);
        PlayerPrefs.SetInt("arrowDamage",arrowDamage);
        PlayerPrefs.SetInt("fireballDamage",fireballDamage);
        PlayerPrefs.SetInt("level",level);
        PlayerPrefs.SetInt("skeletons",skeletons);
    }

    public void loadData()
    {
        maxHealth = PlayerPrefs.GetInt("maxHealth");
        currentHealth = PlayerPrefs.GetInt("currentHealth");
        swordDamage = PlayerPrefs.GetInt("swordDamage");
        arrowDamage = PlayerPrefs.GetInt("arrowDamage");
        fireballDamage = PlayerPrefs.GetInt("fireballDamage");
        level = PlayerPrefs.GetInt("level");
        skeletons = PlayerPrefs.GetInt("skeletons");
    }
}

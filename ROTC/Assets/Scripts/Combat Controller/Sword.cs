using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    int damage;
    int enemyDamage;

    private void Start() 
    {
        enemyDamage = States.instance.getEnemyDamage();
        damage = States.instance.getDamage();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement) && GetComponentInParent<PlayerMovement>())
        {
            enemyMovement.Damage(damage);
        }
        else if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement) && GetComponentInParent<EnemyMovement>())
        {
            playerMovement.Damage(enemyDamage);
        }
    }

    public void upgradeDamage()
    {
        damage++;
        States.instance.setDamage(damage);
        Collection.instance.updateSkulls(-10);
    }
}

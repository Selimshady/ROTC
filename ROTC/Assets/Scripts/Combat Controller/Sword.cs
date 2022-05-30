using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    int damage;

    private void Start() 
    {
        damage = States.instance.getDamage();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
        {
            enemyMovement.Damage(damage);
        }
    }

    public void upgradeDamage()
    {
        damage++;
        States.instance.setDamage(damage);
    }
}

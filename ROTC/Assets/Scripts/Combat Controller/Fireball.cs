using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    int damage;

    private void Awake() {
        //damage = States.instance.getDamage();
        damage = 1;
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
    }
}

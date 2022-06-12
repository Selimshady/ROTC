using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    int damage;
    int enemyDamage;

    public GameObject effectPrefab;

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
            if(other.gameObject.TryGetComponent<WarriorMovement>(out WarriorMovement warriorMovement))
            {
                if(!warriorMovement.getIsBlocking())
                {
                    playerMovement.Damage(enemyDamage);
                }
                else
                {
                    GameObject effect = Instantiate(effectPrefab,other.gameObject.transform.position,Quaternion.identity);
                    Destroy(effect,0.4f);
                }
            }
            else
            {
                playerMovement.Damage(enemyDamage);
            }
        }
    }

    public void upgradeDamage()
    {
        damage++;
        States.instance.setDamage(damage);
        Collection.instance.updateSkulls(-10);
    }
}

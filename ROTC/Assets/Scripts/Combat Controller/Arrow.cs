using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int damage;
    public bool isEnemyArrow;

    private void Start() 
    {
        damage = States.instance.getDamage();
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(!isEnemyArrow)
        {
            if(other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
            {
                enemyMovement.Damage(damage);
                Destroy(gameObject);
            }
            if(other.gameObject.CompareTag("Wall"))
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
        else
        {
            if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                playerMovement.Damage(1);
                Destroy(this.gameObject);
            }
        }
    }
}

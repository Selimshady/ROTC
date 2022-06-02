using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int damage;

    private void Start() 
    {
        damage = States.instance.getDamage();
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
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
}

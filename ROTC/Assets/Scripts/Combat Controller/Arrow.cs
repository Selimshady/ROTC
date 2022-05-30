using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int damage;
    private void Awake() 
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
            Debug.Log("girdi");
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public void upgradeDamage()
    {
        damage++;
        States.instance.setDamage(damage);
    }

}

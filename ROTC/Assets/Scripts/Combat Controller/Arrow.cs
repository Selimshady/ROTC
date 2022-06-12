using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    int damage;
    public bool isEnemyArrow;
    public GameObject effectPrefab;

    private bool isStuck;

    private void Start() 
    {
        damage = States.instance.getDamage();
    }
    
    private void OnCollisionEnter2D(Collision2D other) // Player's arrow
    {
        if(!isEnemyArrow && !isStuck)
        {
            if(other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement)) // damage enemy
            {
                enemyMovement.Damage(damage);
                Destroy(gameObject);
            }   
            if(other.gameObject.CompareTag("Wall")) // stuck on walls
            {
                isStuck = true;
                Vector3 pos = transform.position + transform.right/4;
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                GameObject effect = Instantiate(effectPrefab,pos,Quaternion.identity);
                Destroy(effect,0.4f);
            }
        }
    }   

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(isEnemyArrow)
        {
            if(other.gameObject.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
            {
                playerMovement.Damage(1);
                Destroy(this.gameObject);
            }
            else if(other.gameObject.CompareTag("Shield"))
            {
                Destroy(this.gameObject);
                GameObject effect = Instantiate(effectPrefab,other.gameObject.transform.position,Quaternion.identity);
                Destroy(effect,0.4f);
            }
        }
    }
}

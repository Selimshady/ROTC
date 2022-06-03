using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    int damage;
    public GameObject impactEffect;

    private void Start() 
    {
        damage = States.instance.getDamage();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
        {
            enemyMovement.Damage(damage);
            GameObject tmpImpact = Instantiate(impactEffect,transform.position,transform.rotation);
            Destroy(tmpImpact,1f);
            Destroy(this.gameObject);
        }
        if(other.gameObject.CompareTag("Wall"))
        {
            GameObject tmpImpact = Instantiate(impactEffect,transform.position,transform.rotation);
            Destroy(tmpImpact,1f);
            Destroy(this.gameObject);
        }
    }
}

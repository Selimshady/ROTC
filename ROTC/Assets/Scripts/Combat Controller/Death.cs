using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            other.gameObject.GetComponent<PlayerMovement>().Damage(100);
        }
        else if(other.gameObject.GetComponent<EnemyMovement>())
        {
            other.gameObject.GetComponent<EnemyMovement>().Damage(100);
        }
    }
}

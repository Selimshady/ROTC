using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3AttackTrigger : MonoBehaviour
{
    public Enemy3Movement enemy;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            enemy.Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            enemy.endOfAttack();
        }
    }
}

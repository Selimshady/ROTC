using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.TryGetComponent<Health>(out Health playerHealth))
        {
            playerHealth.Damage(2);
        }
        Destroy(this.gameObject,0.01f);
    }
}

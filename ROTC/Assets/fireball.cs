using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public GameObject  fireBall;
    private float coolDown;
    private bool flag;
    private GameObject[] fireBalls;

    private void Update() 
    {
        if(flag)
        {
            instantiateFireball();
        }
    }

    public void instantiateFireball()
    {
        GameObject fireball = Instantiate(fireBall,transform.position,Quaternion.identity);
        fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(5f,0f);
        flag = false;
        Destroy(fireBall,2f);
    }

    public void turnFlag()
    {
        flag = true;
    }
}

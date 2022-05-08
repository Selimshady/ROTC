using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4Movement : EnemyMovement
{
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() 
    {
        ChangeAnimations(); // Control change in animation.   
        Move();
    }

    private void ChangeAnimations()
    {
        if(isDeath)
        {
            ChangeAnimationState(DEATH);
        }
        else if(isGettingHit)
        {
            ChangeAnimationState(HIT);
        }
        else
        {
            ChangeAnimationState(RUN);
        }
    }

    private void Move()
    {
        if(!(isDeath || isGettingHit))
            rb.velocity = new Vector2(-1 * moveSpeed, rb.velocity.y);    
    }
}

    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Movement : EnemyMovement
{
    public bool isAttacking;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    protected void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        isAttacking = true;
    }

    private void FixedUpdate() 
    {
        ChangeAnimations(); // Control change in animation.   
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
        else if(isAttacking)
        {
            ChangeAnimationState(ATTACK + "3");
        }
        else
        {
            ChangeAnimationState(IDLE);
        }
    }    

    public void endOfAttack()
    {
        isAttacking = false;
    }

}

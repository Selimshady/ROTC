using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Movement : EnemyMovement
{
    private bool mustTurn;
    private bool mustTurnToo;

    [HideInInspector]
    public bool mustPatrol;

    public Transform frontPoint;
    
    public LayerMask endPoint;

    private float cooldownToTurn;
    // Start is called before the first frame update

    private Transform activeTransform;
    private bool isAttacking;

    protected void Start()
    {
        cooldownToTurn = 0.2f;
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDeath)
        {
            activeTransform = SwapController.instance.getActive().transform; 
            if(Vector2.Distance(transform.position, activeTransform.position) < 5)
            {
                mustPatrol = false;
                if(transform.position.x - activeTransform.position.x > 0)
                {
                    moveSpeed = -1 * Mathf.Abs(moveSpeed);
                }
                else
                {
                    moveSpeed = Mathf.Abs(moveSpeed);
                }
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                if(Vector2.Distance(transform.position, activeTransform.position) < 2)
                    isAttacking = true;
            }
            else
            {
                mustPatrol = true;
                Patrol();
            }
            cooldownToTurn-=Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        ChangeAnimations(); // Control change in animation.   
        if(mustPatrol)
        {
            mustTurn = Physics2D.OverlapCircle(frontPoint.position,0.1f,endPoint);
            mustTurnToo = Physics2D.OverlapCircle(frontPoint.position,0.1f,groundObjects);
        }
        FlipCharacter();
    }

    private void Patrol()
    {
        if((mustTurn || mustTurnToo) && cooldownToTurn < 0)
        {
            Flip(); 
        }
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        mustPatrol = false;
        moveSpeed *= -1;
        mustPatrol = true;
        cooldownToTurn = 0.2f;
    }

    protected void FlipCharacter()
    {
        if((moveSpeed > 0 && !facingRight) || (moveSpeed < 0 && facingRight))
        {
            facingRight =!facingRight;
            transform.Rotate(0,180f,0);
        }
    }

    private void ChangeAnimations()
    {
        if(isDeath)
        {
            ChangeAnimationState(DEATH);
        }
        else if(isGettingHit)
        {
            if(isAttacking)
                isAttacking = false;
            ChangeAnimationState(HIT);
        }
        else if(isAttacking)
        {
            ChangeAnimationState(ATTACK + "1");
        }
        else
        {
            ChangeAnimationState(RUN);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(frontPoint.position,0.1f);
    }

    public void endOfAttack()
    {
        isAttacking = false;
    }
}

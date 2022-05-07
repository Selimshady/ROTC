using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMovement : PlayerMovement
{
    [Header("Combat")]
    public GameObject ballPref;
    public Transform spawnPos;
    public float ballSpeed;
    private bool isAttacking;
    
    protected override void FixedUpdate() 
    {
        base.FixedUpdate();

        Move();
        ChangeAnimations();
    }

    protected override void InputProcess()
    {
        base.InputProcess();

        if(Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping)
        {
            rb.velocity = new Vector2(0f,jumpForce);
            isJumping = false;
        }
    }

    private void ChangeAnimations()
    {
        if(isDeath)
        {
            ChangeAnimationState(DEATH);
        }
        else if(isAttacking)
        {
            ChangeAnimationState(ATTACK);
        }
        else if(isGrounded)
        {
            if(moveDirection != 0)
            {
                ChangeAnimationState(RUN);
            }
            else
            {
                ChangeAnimationState(IDLE);
            }
        }
        else
        {
            if(rb.velocity.y > 0)
            {
                ChangeAnimationState(JUMP);
            }
            else if(rb.velocity.y < 0)
            {
                ChangeAnimationState(FALL);
            }
        }
    }

    //Controlled by animator.
    public void endAttack()
    {
        isAttacking = false;
    }
    
    private void Spawn()
    {
        GameObject ball = Instantiate(ballPref,spawnPos.position,transform.rotation);
        if(facingRight)
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed * 2,ball.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            ball.GetComponent<Rigidbody2D>().velocity = new Vector2(ballSpeed * -2,ball.GetComponent<Rigidbody2D>().velocity.y);
        }
        Destroy(ball,2f);
    }

}

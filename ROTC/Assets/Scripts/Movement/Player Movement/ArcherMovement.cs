using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : PlayerMovement
{
    [Header("Combat")]
    public GameObject arrowPref;
    public Transform spawnPos;
    public float arrowSpeed;
    private bool isStartStreching;
    private bool isReleased;
    private bool isHolding;
    private bool shotEnabled;
    private bool isAttacking;

    protected void Start()
    {
        isReleased = true; // Arrow is free to shot.
        shotEnabled = true; // Can shoot.
    }

    protected override void FixedUpdate() 
    {
        base.FixedUpdate();
        
        ChangeAnimations();
        Move();    
    }

    protected override void InputProcess()
    {
        base.InputProcess();

        if(Input.GetMouseButtonDown(0) && shotEnabled)
        {
            shotEnabled = false;
            isStartStreching = true;
            isReleased = false;
            isHolding = true;
            isAttacking = true;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isHolding = false;
            animator.speed = 1;
        }
    }

    private void Spawn()
    {
        GameObject arrow = Instantiate(arrowPref,spawnPos.position,transform.rotation);
        if(facingRight)
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * 2,arrow.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            arrow.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowSpeed * -2,arrow.GetComponent<Rigidbody2D>().velocity.y);
        }
        Destroy(arrow,2f);
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
        else if(isStartStreching)
        {
            ChangeAnimationState(ATTACK);
            isStartStreching = false;
        }
        else if(isReleased)
        {
            if(isGrounded)
            {
                if(moveDirection != 0)
                {
                    ChangeAnimationState(RUN);
                }
                else
                    ChangeAnimationState(IDLE);
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
    }
    
    //Controlled by animator.
    public void Release()
    {
        shotEnabled = true;
        isReleased = true;
        isAttacking = false;
    }

    public void holdBowStreched()
    {
        if(isHolding)
            animator.speed = 0;
    }

    public bool getIsAttacking()
    {
        return isAttacking;
    }
}

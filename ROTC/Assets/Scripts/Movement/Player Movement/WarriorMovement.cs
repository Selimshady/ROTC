using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : PlayerMovement
{
    private const string BLOCK = "Block";

    //Combat
    private int hitCount;
    private bool isAttacking;
    private bool isAttackPressed;
    private bool isBlocking;
    private bool isBlockPressed;
    private float coolDown;
    [SerializeField] private float maxAttackCoolDown;
    [SerializeField] private float maxBlockTimer;

    protected void Start()
    {
        facingRight = true;
    }

    protected override void Update() 
    {
        base.Update();
        CoolDown();
    }
    
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        ChangeAnimations(); // Control change in animation.
        Move(); // character move with rigidBody
    }

    protected override void InputProcess()
    {   
        base.InputProcess();

        if(Input.GetMouseButtonDown(0))
        {
            if(hitCount != 0)
            {
                if(coolDown >= maxAttackCoolDown/2)
                {
                    isAttacking = true;
                    isAttackPressed = true;
                }
            }
            else
            {
                isAttacking = true;
                isAttackPressed = true;
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            isBlockPressed = true;
        }
        if(Input.GetMouseButtonUp(1))
        {
            isBlockPressed = false;
            isBlocking = false;
        }
    }

    private void Move()
    {
        if(!isBlocking)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            if(isJumping)
            {
                rb.velocity = new Vector2(0f,jumpForce);
                isJumping = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(0,rb.velocity.y);
            isJumping = false;
            isAttackPressed = false;
        }
    }


    private void ChangeAnimations()
    {
        if(isBlockPressed)
        {
            if(!isBlocking)
            {
                ChangeAnimationState(BLOCK);
                isBlocking = true;
            }
        }
        else if(!isAttacking)
        {
            if(isGrounded)
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
        else if(isAttackPressed)
        {
            hitCount++;
            isAttackPressed = false;
            if(coolDown <= maxAttackCoolDown && hitCount <= 3) // WARNING
            {
                ChangeAnimationState(ATTACK + hitCount);
                coolDown = 0;
            }
        }
    }

    private void CoolDown()
    {
        coolDown += Time.deltaTime;
        if(coolDown > maxAttackCoolDown)
        {
            hitCount = 0;
            coolDown = 0;
            isAttacking = false;
        }
    }
}
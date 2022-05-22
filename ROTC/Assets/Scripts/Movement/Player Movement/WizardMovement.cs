using System;
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

    [Header("Teleporting")]
    private bool isSearching;
    private float teleportCooldown;
    private float timer;
    private Camera cam;
    public GameObject teleportRangeCenter;
    public LayerMask teleportLayer;
    public GameObject teleportPoint;


    protected override void Awake() 
    {
        base.Awake();
        teleportCooldown = 4f;
        timer = teleportCooldown;
        cam = Camera.main;
    }
    
    protected override void FixedUpdate() 
    {
        base.FixedUpdate();

        Move();
        ChangeAnimations();
        timer-=Time.fixedDeltaTime;
    }

    protected override void InputProcess()
    {
        base.InputProcess();

        if(Input.GetMouseButtonDown(0) && !isSearching)
        {
            isAttacking = true;
        }
        if(Input.GetMouseButtonDown(1) && !isAttacking && isGrounded) 
        {
            isSearching = true;
            teleportRangeCenter.GetComponent<SpriteRenderer>().enabled = true;
        }
        if(Input.GetMouseButtonUp(1) && isSearching)
        {
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            
            if (Vector2.Distance(mousePos, teleportRangeCenter.transform.position) < 5f && timer <= 0) 
            {
                transform.position = new Vector3(mousePos[0],mousePos[1],0f);
                timer = teleportCooldown;
                Instantiate(GetComponentInParent<SwapController>().wizardEffect,transform.position,Quaternion.identity);
            }
                    
            isSearching = false;
            teleportRangeCenter.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    /*protected virtual void OnDrawGizmos() 
    { // to be able to see the groundCheck radius
        Gizmos.DrawWireSphere(teleportRangeCenter.transform.position,5f);
    }*/

    private void Move()
    {
        if(!isSearching)
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
        }
    }

    private void ChangeAnimations()
    {
        if(isDeath)
        {
            ChangeAnimationState(DEATH);
        }
        else if(isSearching)
        {
            ChangeAnimationState(IDLE);
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

    public bool getIsAttacking()
    {
        return isAttacking;
    }
}

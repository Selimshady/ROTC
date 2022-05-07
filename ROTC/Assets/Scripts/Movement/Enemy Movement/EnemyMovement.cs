using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Health")]
    public Health enemyHealth;
    
    protected Rigidbody2D rb;
    protected Animator animator; // to control in change animations
    protected string currentState; // current Animation playing

    [Header("Basic Movement")]
    public float moveSpeed;  // Character speed
    public float jumpForce; // The indicator of how much the character can jump
    public Transform ceilingCheck; 
    public Transform groundCheck;
    public LayerMask groundObjects; // Where can character reset its jumpCount
    public float checkRadius; //To understand if character is grounded

    protected bool facingRight; // For animation 
    protected bool isGrounded; // to reset jumpCount
    protected bool isJumping; // To make character jump
    protected bool isGettingHit; // Check if get damage
    protected bool isDeath; // to check if the player is dead.

    //Animation States;
    protected const string IDLE = "Idle";
    protected const string JUMP = "Jump";
    protected const string RUN = "Run";
    protected const string ATTACK = "Attack";
    protected const string HIT = "GetHit";
    protected const string DEATH = "Death";

    protected void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position,new Vector2(0.7f,checkRadius),0f,groundObjects);
    }

    protected void ChangeAnimationState(string newState)
    {
        if(currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }

    public void Damage(int damage)
    {
        if(enemyHealth.Damage(damage))
        {
            GetComponent<Rigidbody2D>().simulated = false;
            isDeath = true;
        }
        else
        {
            isGettingHit = true;
        }
    }

    public void DamageEnd()
    {
        isGettingHit = false;
    }
}

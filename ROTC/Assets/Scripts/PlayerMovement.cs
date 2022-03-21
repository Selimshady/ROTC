using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Basic Movement")]
    public float moveSpeed;  // Character speed
    public float jumpForce; // The indicator of how much the character can jump
    public Transform ceilingCheck; 
    public Transform groundCheck;
    public LayerMask groundObjects; // Where can character reset its jumpCount
    public float checkRadius; //To understand if character is grounded
    
    private bool facingRight; // For animation
    private float moveDirection; //For animation
    private bool isJumping; // To make character jump
    private bool isGrounded; // to reset jumpCount
    private int maxJumpCount = 1;
    private int jumpCount; //Character can jump more than once

    private Animator animator;
    private string CurrentState;


    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Start() 
    {
        facingRight = true;
        jumpCount = maxJumpCount; //  Jump count can be more than 1.
    }

    private void Update()
    {
        InputProcess();//Gets input values about jumping and moving.
        CheckGround();//Checks if character is on air or not.
        Animate();//Change direction of character.
    }

    
    private void FixedUpdate()
    {
        Move(); // character move with rigidBody
    }

    private void InputProcess()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Returns an int between [-1,+1] as depending on input.
        if (Input.GetButtonDown("Jump") && jumpCount > 0) //Returns bool.
        {
            isJumping = true;
        }
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position,new Vector2(0.7f,checkRadius),0f,groundObjects);
        if(isGrounded)
        {
            jumpCount = maxJumpCount;
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping)
        {
            rb.velocity = new Vector2(0f,jumpForce);
            jumpCount--;
            isJumping = false;
        }
    }

    private void Animate()
    {
        if(moveDirection > 0 && !facingRight)
        {
            flipCharacter(); // change rotation of character.
        }
        else if(moveDirection < 0 && facingRight)
            flipCharacter();
    }

    private void flipCharacter()
    {
        facingRight =!facingRight;
        transform.Rotate(0f,180f,0f);
    }

    private void OnDrawGizmos() 
    { // to be able to see the groundCheck radius
        Gizmos.DrawWireCube(groundCheck.position,new Vector2(0.8f,checkRadius));
    }

}
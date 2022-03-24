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
    
    private float moveDirection; //For animation
    private int maxJumpCount = 1;
    private int jumpCount; //Character can jump more than once


    private bool facingRight; // For animation 
    private bool isGrounded; // to reset jumpCount
    private bool isJumping; // To make character jump
    private bool isAttacking;

    //Animation
    private Animator animator; // to control in change animations
    private string currentState; // current Animation playing
    public float maxAttackCoolDown;
    private float attackCoolDown;

    //Animation States;
    const string IDLE = "Idle";
    const string FALL = "Fall";
    const string JUMP = "Jump";
    const string RUN = "Run";
    const string ATTACK1 = "Attack1";

    private IEnumerator animCoroutine;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Start() 
    {
        attackCoolDown = maxAttackCoolDown;
        facingRight = true;
        jumpCount = maxJumpCount; //  Jump count can be more than 1.
    }

    private void Update()
    {
        InputProcess();//Gets input values about jumping and moving.
        CheckGround();//Checks if character is on air or not.
        Animate();//Change direction of character.
        changeAnimations(); // Control change in animation.
        attackCoolDown -= Time.deltaTime;
        Debug.Log(attackCoolDown);
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
        if(attackCoolDown <= 0 && Input.GetMouseButtonDown(0))
        {
            attackCoolDown = maxAttackCoolDown;
            isAttacking = true;
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

    private void changeAnimations()
    {
        if(isAttacking)
        {
            changeAnimationState(ATTACK1);
            StartCoroutine(WaitForAttackAnim());
        }
        else if(isGrounded)
        {
            if(moveDirection != 0)
            {
                changeAnimationState(RUN);
            }
            else
            {   
                changeAnimationState(IDLE);
            }
        }
        else
        {
            if(rb.velocity.y > 0)
            {
                changeAnimationState(JUMP);
            }
            else if(rb.velocity.y < 0)
            {
                changeAnimationState(FALL);
            }
        }
    }

    private void changeAnimationState(string newState)
    {
        if(currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }


    IEnumerator WaitForAttackAnim()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private void OnDrawGizmos() 
    { // to be able to see the groundCheck radius
        Gizmos.DrawWireCube(groundCheck.position,new Vector2(0.8f,checkRadius));
    }

}
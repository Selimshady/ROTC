using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorMovement : MonoBehaviour
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
    private int hitCount;

    private bool facingRight; // For animation 
    private bool isGrounded; // to reset jumpCount
    private bool isJumping; // To make character jump
    private bool isAttacking;
    private bool isAttackPressed;
    private bool isBlocking;
    private bool isBlockPressed;


    private float coolDown;
    [SerializeField] private float maxAttackCoolDown;
    [SerializeField] private float maxBlockTimer;
    private float blockTimer;


    //Animation
    private Animator animator; // to control in change animations
    private string currentState; // current Animation playing
    
    //Animation States;
    const string IDLE = "Idle";
    const string FALL = "Fall";
    const string JUMP = "Jump";
    const string RUN = "Run";
    const string BLOCK = "Block";

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();    
    }

    private void Start() 
    {
        facingRight = true;
    }

    private void Update()
    {
        InputProcess();//Gets input values about jumping and moving.
        CoolDown();    
    }

    
    private void FixedUpdate()
    {
        ChangeAnimations(); // Control change in animation.
        Animate();//Change direction of character.
        CheckGround();//Checks if character is on air or not.
        Move(); // character move with rigidBody
    }

    private void InputProcess()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Returns an int between [-1,+1] as depending on input.
        if(Input.GetButtonDown("Jump") && isGrounded) //Returns bool.
        {
            isJumping = true;
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(hitCount != 0)
            {
                if(coolDown >= maxAttackCoolDown/2) // WARNING
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

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position,new Vector2(0.7f,checkRadius),0f,groundObjects);
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

    private void Animate()
    {
        if(moveDirection > 0 && !facingRight)
            FlipCharacter(); // change rotation of character. 
        else if(moveDirection < 0 && facingRight)
            FlipCharacter();
    }

    private void FlipCharacter()
    {
        facingRight =!facingRight;
        transform.Rotate(0f,180f,0f);
    }

    private void ChangeAnimations()
    {
        if(isBlockPressed)
        {
            if(!isBlocking)
            {
                changeAnimationState(BLOCK);
                isBlocking = true;
            }
        }
        else if(!isAttacking)
        {
            if(isGrounded)
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
        else if(isAttackPressed)
        {
            hitCount++;
            isAttackPressed = false;
            if(coolDown <= maxAttackCoolDown && hitCount <= 3) // WARNING
            {
                changeAnimationState("Attack" + hitCount);
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


    private void changeAnimationState(string newState)
    {
        if(currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }

    private void OnDrawGizmos() 
    { // to be able to see the groundCheck radius
        Gizmos.DrawWireCube(groundCheck.position,new Vector2(0.8f,checkRadius));
    }

}
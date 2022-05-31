using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Health")]
    public Health playerHealth;

    protected Rigidbody2D rb;
    protected Animator animator; // to control in change animations
    protected string currentState; // current Animation playing

    protected float moveDirection; //For animation

    [Header("Basic Movement")]
    protected float moveSpeed;  // Character speed
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
    protected const string FALL = "Fall";
    protected const string JUMP = "Jump";
    protected const string RUN = "Run";
    protected const string ATTACK = "Attack";
    protected const string HIT = "GetHit";
    protected const string DEATH = "Death";

    [Header("Collectable")]
    protected int skulls;

    protected virtual void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start() 
    {
        skulls = States.instance.getSkulls();
        moveSpeed = States.instance.getSpeed();
    }

    private void OnEnable() 
    {
        if(States.instance != null)
            moveSpeed = States.instance.getSpeed();
    }

    protected virtual void Update()
    {       
        if(!isDeath)
            InputProcess();//Gets input values about jumping and moving.  
    }

    protected virtual void FixedUpdate()
    {
        FlipCharacter();//Change direction of character.
        CheckGround();//Checks if character is on air or not.
    }

    protected virtual void InputProcess()
    {
        moveDirection = Input.GetAxis("Horizontal"); // Returns an int between [-1,+1] as depending on input.
        if(Input.GetButtonDown("Jump") && isGrounded) //Returns bool.
        {
            isJumping = true;
        }
    }

    protected void FlipCharacter()
    {
        if((moveDirection > 0 && !facingRight) || (moveDirection < 0 && facingRight))
        {
            facingRight =!facingRight;
            transform.Rotate(0f,180f,0f);
        }
    }

    protected void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position,new Vector2(0.7f,checkRadius),0f,groundObjects);
    }

    protected virtual void OnDrawGizmos() 
    { // to be able to see the groundCheck radius
        Gizmos.DrawWireCube(groundCheck.position,new Vector2(0.7f,checkRadius));
    }

    public void ChangeAnimationState(string newState)
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
        if(!isGettingHit)
        {
            isGettingHit = true;
            if(playerHealth.Damage(damage))
            {
                isDeath = true;
            }
        }
    }

    public void EndOfHit()
    {
        isGettingHit = false;
    }

    public bool getIsDeath()
    {
        return isDeath;
    }

    public bool getIsGrounded()
    {
        return isGrounded;
    }

    public bool getFacingRight()
    {
        return facingRight;
    }

    public void setFacingRight(bool val)
    {
        facingRight = val;
    }

    public void upgradeSpeed()
    {
        if(this.gameObject.activeInHierarchy)
        {
            moveSpeed++;
            States.instance.setSpeed(moveSpeed);
            skulls-=10;
        }
    }

    public int getSkulls()
    {
        return skulls;
    }


    public void updateSkulls(int gain)
    {
        skulls+=gain;
        NpcInteraction.instance.UpdateUI();
    }
}

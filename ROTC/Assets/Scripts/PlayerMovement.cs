using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject warrior;
    public GameObject archer;
    public GameObject wizard;

    protected Rigidbody2D rb;
    protected Animator animator; // to control in change animations
    protected string currentState; // current Animation playing

    protected float moveDirection; //For animation

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

    //Animation States;
    protected const string IDLE = "Idle";
    protected const string FALL = "Fall";
    protected const string JUMP = "Jump";
    protected const string RUN = "Run";
    protected const string ATTACK = "Attack";

    protected void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Start()// Start is called before the first frame update
    {
        facingRight = true;   
    }

    protected virtual void Update()
    {
        Debug.Log(moveDirection);
        InputProcess();//Gets input values about jumping and moving.  
        ChangeCharacter();
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

    protected void OnDrawGizmos() 
    { // to be able to see the groundCheck radius
        Gizmos.DrawWireCube(groundCheck.position,new Vector2(0.8f,checkRadius));
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


    protected void ChangeCharacter()
    {
        if(isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && !warrior.activeInHierarchy)
            {
                warrior.transform.position = new Vector2(this.gameObject.transform.position.x,warrior.transform.position.y);
                ChangeAnimationState(IDLE);
                this.gameObject.SetActive(false);
                warrior.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && !archer.activeInHierarchy)
            {
                archer.transform.position = new Vector2(this.gameObject.transform.position.x,archer.transform.position.y);
                ChangeAnimationState(IDLE);
                this.gameObject.SetActive(false);
                archer.SetActive(true);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3) && !wizard.activeInHierarchy)
            {
                wizard.transform.position = new Vector2(this.gameObject.transform.position.x,wizard.transform.position.y);
                ChangeAnimationState(IDLE);
                this.gameObject.SetActive(false);
                wizard.SetActive(true);
            }
        }
    }
}

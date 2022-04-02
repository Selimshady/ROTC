using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    [Header("Combat")]
    public GameObject arrowPref;
    public Transform spawnPos;
    public float arrowSpeed;

    private float moveDirection;
    private bool facingRight;
    private bool isGrounded;
    private bool isJumping;
    private bool isStartStreching;
    private bool isReleased;
    private bool isHolding;
    private bool shotEnabled;

    private Animator animator;
    private string currentState;

    const string IDLE = "Idle";
    const string FALL = "Fall";
    const string JUMP = "Jump";
    const string RUN = "Run";
    const string ATTACK = "Attack";
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }



    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;   
        isReleased = true;
        shotEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        InputProcess();
    }

    private void FixedUpdate() 
    {
        ChangeAnimations();
        Animate();
        CheckGround();
        Move();    
    }

    private void InputProcess()
    {
        moveDirection = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }

        if(Input.GetMouseButtonDown(0) && shotEnabled)
        {
            shotEnabled = false;
            isStartStreching = true;
            isReleased = false;
            isHolding = true;
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
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position,new Vector2(0.7f,checkRadius),0f,groundObjects);
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

    private void Animate()
    {
        if(moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if(moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void FlipCharacter()
    {
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }

    private void ChangeAnimations()
    {
        if(isStartStreching)
        {
            changeAnimationState(ATTACK);
            isStartStreching = false;
        }
        else if(isReleased)
        {
            if(isGrounded)
            {
                if(moveDirection != 0)
                {
                    changeAnimationState(RUN);
                }
                else
                    changeAnimationState(IDLE);
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
    }
    
    //Controller by animator.
    public void Release()
    {
        shotEnabled = true;
        isReleased = true;
    }

    public void holdBowStreched()
    {
        if(isHolding)
            animator.speed = 0;
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

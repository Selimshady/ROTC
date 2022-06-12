using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5Movement : EnemyMovement
{
    private bool isInRange;
    private bool isAttacking;

    public GameObject arrowPrefab;
    public Transform spawnPos;

    private Transform activeTransform;

    public float rotationModifer;

    public float attackCoolDown;
    // Start is called before the first frame update
    protected void Start()
    {
        attackCoolDown = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDeath)
        {
            activeTransform = SwapController.instance.getActive().transform ; 
            if(Vector2.Distance(transform.position, activeTransform.position) < 5)
            {
                isInRange = true;
            }
            else
            {
                isInRange = false;
            }
            
            if(transform.position.x > activeTransform.position.x && facingRight)
            {
                transform.Rotate(0,180f,0);
                facingRight = false;
            }
            else if(transform.position.x < activeTransform.position.x && !facingRight)
            {
                transform.Rotate(0,180f,0);
                facingRight = true;
            }
            attackCoolDown-=Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        ChangeAnimations(); // Control change in animation.   
    }

    private void Spawn()
    {
        Vector2 target = activeTransform.position - spawnPos.position;
        
        float angle;
        if(facingRight)
        {
            angle = Vector2.Angle(new Vector2(1f,0f),new Vector2(target.x,target.y));
            if (target.y < 0.0f) angle = 360.0f - angle;
        }
        else
        {
            angle = Vector2.Angle(new Vector2(-1f,0f),new Vector2(target.x,target.y)) * -1;
            if (target.y > 0.0f) angle = 360.0f - angle;
        }
        spawnPos.Rotate(0f,0f,angle);
        GameObject arrow = Instantiate(arrowPrefab,spawnPos.position,spawnPos.rotation);
        spawnPos.Rotate(0f,0f,-angle);
        Destroy(arrow,3f);
        arrow.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(target) * 10;
    }

    private void ChangeAnimations()
    {
        if(isDeath)
        {
            ChangeAnimationState(DEATH);
        }
        else if(isGettingHit)
        {
            ChangeAnimationState(HIT);
        }
        else if(isInRange && attackCoolDown < 0)
        {
            if(transform.position.y > SwapController.instance.getActive().transform.position.y + 1)
                ChangeAnimationState(ATTACK + "2");
            else
                ChangeAnimationState(ATTACK + "1");
            attackCoolDown = 3f;
        }
        else if(attackCoolDown < 2)
            ChangeAnimationState(IDLE);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DogController : MonoBehaviour
{

    public GameObject playerCat;
    private SpriteRenderer sprt;
    private Animator anim;
    private enum animationState { patrolling, chasing };
    private float speed;
    public float walkspeed;
    public float chasespeed;
    public Rigidbody2D rb;
    //patroll
    private float patrolRight;
    private float patrolLeft;
    //chase
    private float chaseRight;
    private float chaseLeft;

    public float startingx;
    public bool patrolling;
    //limit of distance of patrol
    public float patrolDistance;
    //limit of chase distance
    public float chasingDistance;
    //sight distance
    public float sightDistance;
    // Start is called before the first frame update

    public float maxHealth;
    private float health;
    public float minimumImpactVelocity; // how much relative velocity ball to mouse before takes damage
    public HealthBarController healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.setHealth(health, maxHealth);

        try
        {
            playerCat = GameObject.FindGameObjectWithTag("Cat");
        }
        catch (NullReferenceException ex)
        {
            Debug.Log(ex.Message);
        }


        rb = GetComponent<Rigidbody2D>();
        sprt = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        walkspeed = 200f;
        chasespeed = 400f;
        startingx = rb.GetComponent<Transform>().position.x;
        patrolDistance = 3.5f;
        chasingDistance = 7f;
        sightDistance = 3.5f;
        //patrol true=patrol, patrol false=chasing
        patrolling = true;

        sprt.flipX = false;
        patrolRight = startingx + patrolDistance;
        patrolLeft = startingx - patrolDistance;
        chaseRight = startingx + chasingDistance;
        chaseLeft = startingx - chasingDistance;

    }

    private void checkChase(float currentx)
    {
        //0.check if on the same y
        bool isOnSameY = false;
        if (Math.Abs(Math.Abs(playerCat.transform.position.y) - Math.Abs(rb.position.y)) >= 2f)
            isOnSameY = false;
        else
            isOnSameY = true;

        if (isOnSameY)
        {
            bool catIsWithinSpot = false;
            float disToPlayer;

            //1.cat is within sight distance
            if (currentx <= playerCat.transform.position.x)
                disToPlayer = Math.Abs(playerCat.transform.position.x) - Math.Abs(currentx);
            else
                disToPlayer = Math.Abs(currentx) - Math.Abs(playerCat.transform.position.x);
            if (Math.Abs(disToPlayer) >= sightDistance)
                catIsWithinSpot = false;
            else
                catIsWithinSpot = true;
            //2.spotted =>(dog's vision) to the left side
            if (rb.velocity.x <= 0.01f && catIsWithinSpot)
            {
                //
                if (currentx >= playerCat.transform.position.x)
                {
                    patrolling = false;
                    chasespeed = -400f;
                }
                else
                    patrolling = true;
            }
            else if (rb.velocity.x >= 0.01f && catIsWithinSpot)//to the right side
            {
                if (currentx <= playerCat.transform.position.x)
                {
                    patrolling = false;
                    chasespeed = 400f;
                }
                else
                    patrolling = true;
            }
            else
                patrolling = true;
        }
        else
            patrolling = true;
        //
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float currentx;
        currentx = rb.GetComponent<Transform>().position.x;


        checkChase(currentx);


        //patrolling
        if (patrolling)
        {
            //check for distance
            if (currentx >= patrolRight)
            {
                walkspeed = -200f;
                sprt.flipX = true;
                // Debug.Log(rb.velocity.x);
            }
            if (currentx <= patrolLeft)
            {
                walkspeed = 200f;
                sprt.flipX = false;
                // Debug.Log(rb.velocity.x);
            }

            speed = walkspeed;
        }
        else //chasing
        {
            if (currentx >= chaseRight)
            {
                speed = -200f;
                sprt.flipX = true;
                patrolling = true;
                // Debug.Log(rb.velocity.x);
            }
            else if (currentx <= chaseLeft)
            {
                speed = 200f;
                sprt.flipX = false;
                patrolling = true;
                // Debug.Log(rb.velocity.x);
            }
            else
                speed = chasespeed;
        }




        //change velocity
        Vector2 v2 = new Vector2(speed * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = v2;
        //update animation
        AnimationUpdate();
    }
    private void AnimationUpdate()
    {
        animationState aState;
        if (patrolling)
            aState = animationState.patrolling;
        else
            aState = animationState.chasing;

        anim.SetInteger("AnimationState", (int)aState);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // if (collision.gameObject.tag == "Cat")
        // {
        //     Debug.Log("caught");
        // }

        // if (collision.gameObject.tag == "Ground")
        // {
        //     speed = -speed;
        // }
        // if (collision.gameObject.tag == "wall")
        // {
        //     speed = -speed;
        // }
        // if (collision.gameObject.tag == "yarnball")
        // {
        //     Debug.Log("hit by yarnball");
        // }

        switch (collision.gameObject.tag)
        {
            case "Cat":
                // Debug.Log("caught");
                break;
            // case "Ground": // necessary?
            //     break;
            case "Wall":
                speed = -speed;
                break;
            case "Ball":
                BallController bc = collision.gameObject.GetComponent<BallController>();
                float impactVelocity;
                // bc.rigidBody2D.velocity.magnitude is velocity of the yarn ball
                // collision.relativeVelocity.magnitude is the relative vel between ball and mouse
                // gets minimum of the two so mouse can't kill itself by running into it fastly
                if (bc.rigidBody2D.velocity.magnitude < collision.relativeVelocity.magnitude)
                {
                    impactVelocity = bc.rigidBody2D.velocity.magnitude;
                }
                else
                {
                    impactVelocity = collision.relativeVelocity.magnitude;
                }
                if (impactVelocity > minimumImpactVelocity)
                {
                    float hitStrength = bc.hitStrength; // gets hit strength from yarn ball
                    health = health - hitStrength * impactVelocity;
                    healthBar.setHealth(health, maxHealth);
                    if (health <= 0)
                    {
                        Destroy(this.gameObject.GetComponentInParent<DogController>().gameObject); // removes dog
                    }
                }
                break;
        }

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public float x1; // left position mouse has to walk to in walk cycle
    public float x2; // right position
    public int walkSpeed; // speed for walk cycle
    public int runSpeed; // flee speed
    public bool directionRight; // direction where mouse is moving
    public int state; // state mouse is in
    public Rigidbody2D rb; // going to need to drag mouse's rigid body in here
    public float maxHealth;
    private float health;
    public float minimumImpactVelocity; // how much relative velocity ball to mouse before takes damage
    public float fleeTime; // how long the mouse will flee for
    private float fleeTimeTimer;
    public float checkSafetyTime; // how long the mouse will check its safety
    // Start is called before the first frame update
    private float checkSafetyTimeTimer;
private SpriteRenderer sprt;
    public GameObject cat; // used for the vision
    public float visionDistanceX; // used for how far in x mouse can see
    public float visionDistanceY; // used for how far in y mouse can see
    public HealthBarController healthBar;
    void Start()
    {
        health = maxHealth;
        healthBar.setHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseVelocity = rb.velocity;
        Vector2 mousePosition = rb.position;
        // if sees cat flee
        if (directionRight)
        { // mouse facing right
            if (cat.transform.position.x > mousePosition.x && Mathf.Abs(cat.transform.position.x - mousePosition.x) <= visionDistanceX && Mathf.Abs(cat.transform.position.y - mousePosition.y) <= visionDistanceY)
            { // cat to the right of mouse and is inside vision box
                state = 1;
                fleeTimeTimer = Time.time + fleeTime;
                directionRight = false;
                sprt.flipX = false;

            }
        }
        else
        { // mouse facing left
            if (cat.transform.position.x < mousePosition.x && Mathf.Abs(cat.transform.position.x - mousePosition.x) <= visionDistanceX && Mathf.Abs(cat.transform.position.y - mousePosition.y) <= visionDistanceY)
            { // cat to the left of mouse and is inside vision box
                state = 1;
                fleeTimeTimer = Time.time + fleeTime;
                directionRight = true;       
                 sprt.flipX = true;

            }
        }

        switch (state)
        {
            case 0: // walk cycle
                if (directionRight)
                {
                    if (mousePosition.x >= x2)
                    {
                        directionRight = false;
                        sprt.flipX = false;
                        mouseVelocity.x = -walkSpeed;
                    }
                    else
                    {
                        mouseVelocity.x = walkSpeed;
                    }
                }
                else
                { // direction left
                    if (mousePosition.x <= x1)
                    {
                        directionRight = true;
                        sprt.flipX = true;
                        mouseVelocity.x = walkSpeed;
                    }
                    else
                    {
                        mouseVelocity.x = -walkSpeed;
                    }
                }
                rb.velocity = mouseVelocity;
                break;
            case 1: // flee (will be on a timer)
                if (Time.time > fleeTimeTimer)
                { // done fleeing
                    state = 2;
                    checkSafetyTimeTimer = Time.time + checkSafetyTime;
                    // face opposite direction
                }
                if (directionRight)
                {
                    mouseVelocity.x = runSpeed;
                }
                else
                {
                    mouseVelocity.x = -runSpeed;
                }
                rb.velocity = mouseVelocity;
                break;
            case 2: // check safety
                if (Time.time > checkSafetyTime)
                { // safe to return to roam
                    state = 0;
                    // picks the larger distance to travel back to
                    if (Mathf.Abs(x1 - mousePosition.x) > Mathf.Abs(x2 - mousePosition.x))
                    {
                        directionRight = false;
                        sprt.flipX = false;
                    }
                    else
                    {
                        directionRight = true;
                        sprt.flipX = true;
                    }
                }
                break;
        }
    }
    

    // if collides with cat --> flee (cat walked up behind mouse)
    // if collides with yarnball --> flee & take damage
    // will need to set the timer here
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) // tags are case sensitive
        {
            case "Cat":
                state = 1;
                fleeTimeTimer = Time.time + fleeTime;
                if (rb.position.x < collision.transform.position.x)
                { // mouse to left of cat, run left
                    directionRight = false;
                }
                else
                { // mouse to right of cat, run right
                    directionRight = true;
                }
                break;
            case "Ball": // force of ball used in calculating damage (also if light tap of ball don't flee or take damage)
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
                    { // dead mouse 5
                        SpriteRenderer m_SpriteRenderer = GetComponent<SpriteRenderer>();
                        m_SpriteRenderer.color = Color.black;
                        Destroy(this); // removes the behavior script
                    }
                    state = 1;
                    fleeTimeTimer = Time.time + fleeTime;
                    if (rb.position.x < collision.transform.position.x)
                    { // mouse to left of ball, run left
                        directionRight = false;
                    }
                    else
                    { // mouse to right of ball, run right
                        directionRight = true;
                    }
                }
                break;
        }
    }
}

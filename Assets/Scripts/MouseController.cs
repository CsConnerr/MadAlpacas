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
    public float health;
    public float minimumImpactVelocity; // how much relative velocity ball to mouse before takes damage
    public float fleeTime; // how long the mouse will flee for
    private float fleeTimeTimer;
    public float checkSafetyTime; // how long the mouse will check its safety
    // Start is called before the first frame update
    private float checkSafetyTimeTimer;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseVelocity = rb.velocity;
        Vector2 mousePosition = rb.position;
        switch (state)
        {
            // if sees cat --> set state = 1, set direction
            case 0: // walk cycle
                if (directionRight)
                {
                    if (mousePosition.x >= x2)
                    {
                        directionRight = false;
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
                    if (Mathf.Abs(x1 - mousePosition.x) > Mathf.Abs(x2 - mousePosition.x)) {
                        directionRight = false;
                    } else {
                        directionRight = true;
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
                    if (health <= 0)
                    {
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

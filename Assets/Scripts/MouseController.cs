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

    public int health;

    // Start is called before the first frame update
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
                if (rb.position.x < collision.transform.position.x)
                { // mouse to left of cat, run left
                    directionRight = false;
                }
                else
                { // mouse to right of cat, run right
                    directionRight = true;
                }
                break;
            case "Ball": // force of ball used in calculating damage
                state = 1;
                if (rb.position.x < collision.transform.position.x)
                { // mouse to left of ball, run left
                    directionRight = false;
                }
                else
                { // mouse to right of ball, run right
                    directionRight = true;
                }
                break;
        }
    }
}

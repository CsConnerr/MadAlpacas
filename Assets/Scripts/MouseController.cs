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

    public Vector2 mouseXY;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {

            case 0: // walk cycle
                mouseXY = rb.position;
                Vector2 mouseVelocity = rb.velocity;
                Vector2 mousePosition = rb.position;
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
            case 1: // flee
                break;
            case 2: // check safety
                break;
        }
    }
}

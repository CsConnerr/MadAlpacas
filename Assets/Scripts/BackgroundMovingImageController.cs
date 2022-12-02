using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovingImageController : MonoBehaviour
{
    public bool isGoingRight;
    public float speed;
    private float velocity;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        rb.gravityScale = 0;
        if (isGoingRight)
        {
            velocity = speed;
        }
        else
        {
            velocity = -speed;
        }
        rb.AddForce(velocity * transform.right);
    }


}

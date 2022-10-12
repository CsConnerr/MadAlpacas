using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 v = rb.velocity;
        if (Input.GetAxis("Horizontal") < 0)
        {
            v.x = -speed;
        } 
        else if (Input.GetAxis("Horizontal") > 0)
        {
            v.x = speed;
        } else
        {
            v.x = 0;
        }
        rb.velocity = v; // temp variable needed because can't modify rb.velocity.x directly



        if (Input.GetAxis("Vertical") > 0 && isGrounded)
        {
            rb.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
            isGrounded = false;
            // not grounded
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            // Debug.Log("Grounded");
        }
    }
}

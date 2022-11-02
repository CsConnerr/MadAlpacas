using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public bool isGrounded;
   // private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
    //anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
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

// this is supposed to be for animation transitioj but IDK
/* if(Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("Run", true);
        }
        else
        {
            anim.SetBool("Run", false);
        } */

if (Input.GetAxis("Horizontal") > 0.01f)
            transform.localScale = new Vector3(50, 50, 50);
        else if (Input.GetAxis("Horizontal") < -0.01f)
            transform.localScale = new Vector3(-50, 50, 50);

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
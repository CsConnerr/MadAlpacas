using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CatController : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    public Rigidbody2D rb;
    public bool isGrounded;
    // private Animator anim;
    public float maxHealth;
    private float health;

    public HealthBarController healthBar;

    public float damageFromDog;
    public float dogKnockbackForce;
    public float healthFromMouse;

    private bool canMove;

    public float knockBackTime;

    private float knockBackTimer;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        canMove = true;
        health = maxHealth;
        healthBar.setHealth(health, maxHealth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restarts
        }
        if (canMove)
        {
            Vector2 v = rb.velocity;
            // x
            if (Input.GetAxis("Horizontal") > 0)
            {
                v.x = Mathf.Max(v.x, speed); // max because otherwise when knockback it feels weird
            }
            else if (Input.GetAxis("Horizontal") < 0)
            {
                v.x = Mathf.Min(v.x, -speed);
            }
            else
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
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Ground":
                isGrounded = true;
                break;
            case "Mouse":
                if (collision.gameObject.GetComponent<MouseController>() == null)
                { // no mouse controller therefore mouse is dead
                    Destroy(collision.gameObject); // removes mouse from scene
                    health += healthFromMouse;
                    if (health > maxHealth)
                    {
                        health = maxHealth;
                    }
                    healthBar.setHealth(health, maxHealth);
                }
                break;
            case "Dog":
                health -= damageFromDog;
                if (health <= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restarts
                }
                healthBar.setHealth(health, maxHealth);
                // coroutine needed because otherwise velocity in x-direction doesn't work (and also stuns cat)
                StartCoroutine(applyKnockback(collision.gameObject.GetComponent<Rigidbody2D>().position));
                break;
        }
    }


    public IEnumerator applyKnockback(Vector2 dogPosition)
    {
        GetComponent<CatController>().canMove = false;

        // knockback force
        Vector2 dogCollisionDirection = (rb.position - dogPosition).normalized;
        if (dogCollisionDirection.y < .1)
        {
            dogCollisionDirection.y = .1f;
        }
        Debug.Log(dogKnockbackForce * dogCollisionDirection);
        rb.AddForce(dogKnockbackForce * dogCollisionDirection, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockBackTime);
        GetComponent<CatController>().canMove = true;
        // you can reset the velocity to zero here if you want
    }
}
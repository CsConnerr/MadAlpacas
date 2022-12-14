using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    public Camera camera; // used for testing with mouse
    public Rigidbody2D rigidBody2D; // reference the rigid body
    public float hitStrength; // maybe make it so cat can charge up hit (holding its arm all the way back)

    public int nextLevel;
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public float volumeScaleFactor;
    public bool isSoundPlayed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.spatialBlend = 0.5f; // makes the collision sound more relative to location of ball
    }

    // Update is called once per frame
    // void Update()
    // {

    //     if (Input.GetMouseButtonDown(0)) // left click (in reality the cats arm and where it hits will determine where the ball will move, but this is for testing)
    //     {

    //         Vector2 mouseWorldPosition = camera.ScreenToWorldPoint((Vector2)Input.mousePosition); // mouse position in world (just for testing)
    //         // mouse position in world
    //         // Debug.Log("mouseWorldX = " + mouseWorldPosition.x +
    //         //     ", mouseWorldY = " + mouseWorldPosition.y);
    //         // // ball position
    //         // Debug.Log("ballX = " + transform.position.x +
    //         //     ", ballY = " + transform.position.y);


    //         rigidBody2D.AddForce(hitStrength * (mouseWorldPosition - (Vector2)transform.position).normalized, ForceMode2D.Impulse); // impulse means force applied instantly
    //         // normalized so that it's only a directional vector
    //     }
    // }

    // damage is now being applied in the dog/mouse scripts
    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     switch (collision.gameObject.tag) // tags are case sensitive
    //     {
    //         case "Dog":
    //             // damage the dog
    //             Debug.Log("Dog collision: Damage dog");
    //             break;
    //         case "Mouse":
    //             // kill the mouse
    //             Debug.Log("Mouse collision: Kill mouse");
    //             break;
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.relativeVelocity.magnitude / volumeScaleFactor); // divided by scaling factor
        audioSource.volume = collision.relativeVelocity.magnitude / volumeScaleFactor; // divided by scaling factor
        audioSource.Play();
        //if it hits the dog or mouse, it will play the sound

        if (collision.gameObject.tag == "Dog" || collision.gameObject.tag == "Mouse")
         {
          if(isSoundPlayed  = true) 
          {
           audioSource2.Play();
           isSoundPlayed  = true;
           }
      }
      else 
         audioSource2.Stop();
  }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Hoop")
        {
            // If velocity.y negative --> next level
            if (rigidBody2D.velocity.y < 0)
            {
                Debug.Log("Hoop collision: Next level since you entered from the top. Good boy");
                SceneManager.LoadScene(nextLevel); // restarts
            }
            else
            { // can remove this else statement, just showing that the ball came from the bottom
                Debug.Log("Hoop collision: You entered from the bottom. Bad boy");
            }
        }
    }

}

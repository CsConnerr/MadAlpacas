using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrackerArrowController : MonoBehaviour
{

    public GameObject ball;
    public float arrowHeight;
    public float ballOutOfSightHeight;
    private int state;
    // private float arrowScaleX;
    // public float arrowScaleYFactor; // how long arrow gets when far awway
    // Start is called before the first frame update
    void Start()
    {
        // arrowScaleX = this.transform.localScale.x;
        state = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case 0:
                if (ball.transform.position.y >= ballOutOfSightHeight)
                { // ball out of sight
                    state = 1;
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                }
                break;
            case 1:
                this.transform.position = new Vector3(ball.transform.position.x, arrowHeight, 0f);
                // this.transform.localScale = new Vector3(arrowScaleX, (ball.transform.position.y-ballOutOfSightHeight) * arrowScaleYFactor, 0f); // looks weird not going to use
                if (ball.transform.position.y < ballOutOfSightHeight)
                { // ball in sight
                    state = 0;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                }
                break;
        }
    }
}

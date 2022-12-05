

// three minute countdown timer in unity 2d when timer reaches 0, 
//restart the scene

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 180;
    public bool timerIsRunning = false;
    public Text timeText;

    void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // restarts
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

// Path: Timer.cs
// Compare this snippet from MouseController.cs:
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// 
// public class MouseController : MonoBehaviour
// {
//     public float x1; // left position mouse has to walk to in walk cycle
//     public float x2; // right position
//     public int walkSpeed; // speed for walk cycle
//     public int runSpeed; // flee speed
//     public bool directionRight; // direction where mouse is moving
//     public int state; // state mouse is in
//     public Rigidbody2D rb; // going to need to drag mouse's rigid body in here
//     public float maxHealth;
//     private float health;
//     public float minimumImpactVelocity; // how much relative velocity ball to mouse before takes damage
//     public float fleeTime; // how long the mouse will flee for
//     private float fleeTimeTimer;
//     public float checkSafetyTime; // how long the mouse will check its safety
//     // Start is called before the first frame update
//     private float checkSafetyTimeTimer;
//     private SpriteRenderer sprt;
//     public GameObject cat; // used for the vision
//     public float visionDistanceX
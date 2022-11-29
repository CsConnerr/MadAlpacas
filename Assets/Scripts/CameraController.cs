using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cat;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (cat.transform.position.x <= 15)
        {
            this.transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            this.transform.position = new Vector3(30, 0, -10);
        }
    }
}

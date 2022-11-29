using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject cat;
    public float minX;
    public float maxX;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        // if (cat.transform.position.x <= 15)
        // {
        //     this.transform.position = new Vector3(0, 0, -10);
        // }
        // else
        // {
        //     this.transform.position = new Vector3(30, 0, -10);
        // }

        transform.position = new Vector3(Mathf.Clamp(cat.transform.position.x, minX, maxX), transform.position.y, transform.position.z);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private RawImage image;
    [SerializeField] private float xVal, yVal;
    
    // Update is called once per frame
    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(xVal, yVal) * Time.deltaTime, image.uvRect.size);
    }
}

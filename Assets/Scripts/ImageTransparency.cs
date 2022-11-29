using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ImageTransparency : MonoBehaviour
{

    // nice script but doesn't look good with the trees
    public bool isSingleImage; // true for one image, false for set of images under a parent
    public float alpha;
    // Start is called before the first frame update
    void Start()
    {
        if (isSingleImage)
        { // script is in single image
            this.transform.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha); // gets sprite and sets transparency
        }
        else
        { // script is in parent of multiple images
            SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer spriteRenderer in spriteRenderers)
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, alpha); // sprite and transparency
            }
        }
    }
}

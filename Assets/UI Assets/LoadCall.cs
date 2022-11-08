using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCall : MonoBehaviour
{
    private bool isFirstUpdate = true;

    // Update is called once per frame
    private void Update()
    {
        if(isFirstUpdate) {
            isFirstUpdate = false;
            SceneLoader.LoaderCallback();
        }
    }
}

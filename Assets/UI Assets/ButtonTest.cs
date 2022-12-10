using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonTest : MonoBehaviour, ISelectHandler
{
    public int level; 
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Switching to Level..." + level.ToString());

        SceneManager.LoadScene("Level " + level.ToString());
    }
}

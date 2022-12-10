using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ReturnToMenu : MonoBehaviour, ISelectHandler
{ 
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("Returning to Menu...");

        SceneManager.LoadScene("MainMenu");
    }
}

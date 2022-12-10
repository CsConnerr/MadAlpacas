using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void RestartLevel(int level)
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level " + level);
    }
    
    public void LoadMenu() 
    {
        Debug.Log("Loading Main Menu");
        GameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

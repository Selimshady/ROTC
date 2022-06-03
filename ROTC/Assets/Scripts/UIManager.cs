using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu, gameOverMenu;
    public static bool isPaused;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isPaused);
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
                Pause();
            else
                Resume();
        }
        
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void Resume()
    {
        if (isPaused)
        {
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            gameOverMenu.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void BacktoMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseMenu, gameOverMenu;
    public static UIManager instance;
    public bool inputAvailable;
    public bool upgradeMenuActive;

    void Start()
    {
        instance = this;
        inputAvailable = true;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !upgradeMenuActive)
        {
            if(inputAvailable)
                Pause();
            else
                Resume();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        inputAvailable = false;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        inputAvailable = true;
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void BacktoMainMenu()
    {
        Time.timeScale = 1;
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

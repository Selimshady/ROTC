using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button newGameButton;
    public Button loadGameButton;
    public Button exitGameButton;

    // Start is called before the first frame update
    void Start()
    {
        if(!States.instance.CheckSaveFile())
        {
            loadGameButton.interactable = false;
        }
    }   

    public void NewGame()
    {
        States.instance.NewGame();
    }

    public void LoadSave()
    {
        States.instance.LoadGame();
    }

    public void Exit() 
    {
        Application.Quit();
    }
}

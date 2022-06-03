using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class NpcInteraction : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject canvas;

    public Button[] buttons;
    public TMP_Text skullsText;

    public static NpcInteraction instance;

    public static bool inputAvailable;


    private void Start() 
    {
        instance = this;
        inputAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                UpdateUI();
                inputAvailable = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseUpgradeMenu();
        }
    }
    public void CloseUpgradeMenu()
    {
        canvas.SetActive(false);
        inputAvailable = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            isInRange = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            isInRange = false;
        }    
    }

    public void OpenSelectionMenu()
    { 
        canvas.SetActive(true);
    }

    public void UpdateUI()
    {
        int totalSkulls = Collection.instance.getSkulls();
        skullsText.SetText(totalSkulls.ToString());
        if(totalSkulls < 10)
        {
            for(int i=0;i<buttons.Length;i++)
            {
                buttons[i].interactable = false;
            }
        }
        else
        {
            for(int i=0;i<buttons.Length;i++)
            {
                buttons[i].interactable = true;
            }
        }
    }
}

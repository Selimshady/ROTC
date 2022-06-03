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
    private bool upgradeMenuActive;


    private void Start() 
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey) && UIManager.instance.inputAvailable)
            {
                interactAction.Invoke();
                UpdateUI();
                UIManager.instance.inputAvailable = false;
                UIManager.instance.upgradeMenuActive = true;
            }
            if(Input.GetKeyDown(KeyCode.Escape) && UIManager.instance.upgradeMenuActive)
            {
                Debug.Log("geldi1");
                CloseUpgradeMenu();
            }
        }
    }
    public void CloseUpgradeMenu()
    {
        canvas.SetActive(false);
        UIManager.instance.inputAvailable = true;
        UIManager.instance.upgradeMenuActive = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NpcInteraction : MonoBehaviour
{
    public bool isInRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject canvas;

    // Update is called once per frame
    void Update()
    {
        if(isInRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(false);
        }
    }

    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            isInRange = true;
            Debug.Log("Player now is in range");
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerMovement>())
        {
            isInRange = false;
            Debug.Log("Player is now out of range");
        }    
    }

    public void OpenSelectionMenu()
    { 
        canvas.SetActive(true);
        Debug.Log("menu is open now");
    }
}

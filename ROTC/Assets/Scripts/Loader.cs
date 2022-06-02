using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerMovement>())
        {
            States.instance.saveData();
            if(SceneManager.GetActiveScene().buildIndex == 0)
                SceneManager.LoadScene(1);
            else if(SceneManager.GetActiveScene().buildIndex == 1)
                SceneManager.LoadScene(0);
        }
    }
}

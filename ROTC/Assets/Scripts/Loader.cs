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
            States.instance.setLevel(1 - SceneManager.GetActiveScene().buildIndex);
            if(SceneManager.GetActiveScene().buildIndex == 0)
            {
                States.instance.setSkulls(States.instance.getSkulls() + 10);
                States.instance.saveData();
                SceneManager.LoadScene(1);
            }
            else if(SceneManager.GetActiveScene().buildIndex == 1)
            {
                States.instance.saveData();
                SceneManager.LoadScene(0);
            }
        }
    }
}

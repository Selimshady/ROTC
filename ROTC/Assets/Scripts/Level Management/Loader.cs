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
            if(SceneManager.GetActiveScene().name.Equals("Level 1"))
            {
                States.instance.setLevel("Main Level");
                States.instance.setSkulls(States.instance.getSkulls() + 10);
                States.instance.saveData();
                SceneManager.LoadScene("Main Level");
            }
            else if(SceneManager.GetActiveScene().name.Equals("Main Level"))
            {
                States.instance.setLevel("Level 1");
                States.instance.saveData();
                SceneManager.LoadScene("Level 1");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapController : MonoBehaviour
{

    public GameObject warrior;
    public GameObject archer;
    public GameObject wizard;

    [Header("Effects")]
    public ParticleSystem effect;
    ParticleSystem.MainModule settings;

    private GameObject active;

    private void Start()
    {
        warrior.SetActive(true);
        archer.SetActive(false);
        wizard.SetActive(false);   
        active = warrior;   
        settings = effect.main;
    }

    private void Update() 
    {
        ChangeCharacter();
    }


    protected void ChangeCharacter()
    {
        if(active.GetComponent<PlayerMovement>().getIsGrounded())
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && !warrior.activeInHierarchy)
            {
                warrior.transform.position = new Vector2(active.transform.position.x,warrior.transform.position.y);
                warrior.GetComponent<PlayerMovement>().setFacingRight(active.GetComponent<PlayerMovement>().getFacingRight());
                warrior.transform.rotation = active.transform.rotation;
                active.SetActive(false);
                warrior.SetActive(true);
                active = warrior;
                active.GetComponent<PlayerMovement>().ChangeAnimationState("Idle");
                settings.startColor = Color.red;
                Instantiate(effect,active.transform.position + new Vector3(0,0,-1f),Quaternion.identity);

            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && !archer.activeInHierarchy)
            {
                archer.transform.position = new Vector2(active.transform.position.x,archer.transform.position.y);
                archer.GetComponent<PlayerMovement>().setFacingRight(active.GetComponent<PlayerMovement>().getFacingRight());
                archer.transform.rotation = active.transform.rotation;
                active.SetActive(false);
                archer.SetActive(true);
                active = archer;
                active.GetComponent<PlayerMovement>().ChangeAnimationState("Idle");
                settings.startColor = Color.green;
                Instantiate(effect,active.transform.position + new Vector3(0,0,-1f),Quaternion.identity);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3) && !wizard.activeInHierarchy)
            {
                wizard.transform.position = new Vector2(active.transform.position.x,wizard.transform.position.y);
                wizard.GetComponent<PlayerMovement>().setFacingRight(active.GetComponent<PlayerMovement>().getFacingRight());
                wizard.transform.rotation = active.transform.rotation;
                active.SetActive(false);
                wizard.SetActive(true);
                active = wizard;
                active.GetComponent<PlayerMovement>().ChangeAnimationState("Idle");
                settings.startColor = new Color(255f,0f,255f);
                Instantiate(effect,active.transform.position + new Vector3(0,0,-1f),Quaternion.identity);
            }
        }
    }
}

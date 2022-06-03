using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapController : MonoBehaviour
{

    public GameObject warrior;
    public GameObject archer;
    public GameObject wizard;
    public Cinemachine.CinemachineVirtualCamera cinemachine;

    [Header("Effects")]
    public ParticleSystem warriorEffect;
    public ParticleSystem archerEffect;
    public ParticleSystem wizardEffect;

    private GameObject active;

    private void Start()
    {
        warrior.SetActive(true);
        archer.SetActive(false);
        wizard.SetActive(false);   
        active = warrior;   
    }

    
    private void Update() 
    {
        if(UIManager.instance.inputAvailable && !active.GetComponent<PlayerMovement>().getIsDeath())
            ChangeCharacter();
    }


    protected void ChangeCharacter()
    {
        if(active.GetComponent<PlayerMovement>().getIsGrounded() && !IsBusy())
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) && !warrior.activeInHierarchy)
            {
                warrior.transform.position = new Vector2(active.transform.position.x,active.transform.position.y + 0.5f);
                warrior.GetComponent<PlayerMovement>().setFacingRight(active.GetComponent<PlayerMovement>().getFacingRight());
                warrior.transform.rotation = active.transform.rotation;
                active.SetActive(false);
                warrior.SetActive(true);
                active = warrior;
                ParticleSystem tmp = Instantiate(warriorEffect,active.transform.position + new Vector3(0,0,-1f),Quaternion.identity);
                tmp.transform.parent = active.transform;
                Destroy(tmp.gameObject,2f);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2) && !archer.activeInHierarchy)
            {
                archer.transform.position = new Vector2(active.transform.position.x,active.transform.position.y + 0.5f);
                archer.GetComponent<PlayerMovement>().setFacingRight(active.GetComponent<PlayerMovement>().getFacingRight());
                archer.transform.rotation = active.transform.rotation;
                active.SetActive(false);
                archer.SetActive(true);
                active = archer;
                ParticleSystem tmp = Instantiate(archerEffect,active.transform.position + new Vector3(0,0,-1f),Quaternion.identity);
                tmp.transform.parent = active.transform;
                Destroy(tmp.gameObject,2f);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3) && !wizard.activeInHierarchy)
            {
                wizard.transform.position = new Vector2(active.transform.position.x,active.transform.position.y + 0.5f);
                wizard.GetComponent<PlayerMovement>().setFacingRight(active.GetComponent<PlayerMovement>().getFacingRight());
                wizard.transform.rotation = active.transform.rotation;
                active.SetActive(false);
                wizard.SetActive(true);
                active = wizard;
                ParticleSystem tmp = Instantiate(wizardEffect,active.transform.position + new Vector3(0,0,-1f),Quaternion.identity);
                tmp.transform.parent = active.transform;
                Destroy(tmp.gameObject,1f);
            }
            cinemachine.Follow = active.transform;
        }
    }

    private bool IsBusy()
    {
        return (warrior.GetComponent<WarriorMovement>().getIsAttacking() || archer.GetComponent<ArcherMovement>().getIsAttacking() || 
            wizard.GetComponent<WizardMovement>().getIsAttacking());
    }
}

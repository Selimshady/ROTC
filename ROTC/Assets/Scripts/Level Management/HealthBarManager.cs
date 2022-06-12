using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    private Slider slider;
    public Health healthScript;
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = States.instance.getMaxHealth();
        slider.value = slider.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(slider.value != healthScript.getCurrentHealth())
            slider.value = Mathf.Lerp(slider.value, healthScript.getCurrentHealth(), 5 * Time.deltaTime);
    }
}

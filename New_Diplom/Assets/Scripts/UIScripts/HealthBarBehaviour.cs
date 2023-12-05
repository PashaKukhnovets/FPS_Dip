using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour: MonoBehaviour
{
    private Slider healthBarSlider;
    
    void Start()
    {
        healthBarSlider = this.GetComponent<Slider>();

        healthBarSlider.maxValue = PlayerParameters.GetPlayerMaxHealth();
        healthBarSlider.value = PlayerParameters.GetPlayerCurrentHealth();
    }

    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthBarSlider.value = PlayerParameters.GetPlayerCurrentHealth();
    }
}

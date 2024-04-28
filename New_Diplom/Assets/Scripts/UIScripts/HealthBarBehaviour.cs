using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour: MonoBehaviour
{
    [SerializeField] private BoostBehaviour boostBehaviour;

    private Slider healthBarSlider;
    
    void Start()
    {
        boostBehaviour.updateHealth += UpdateMaxHealh;
        healthBarSlider = this.GetComponent<Slider>();

        healthBarSlider.maxValue = PlayerParameters.GetPlayerMaxHealth();
        healthBarSlider.value = PlayerParameters.GetPlayerCurrentHealth();
    }

    void Update()
    {
        UpdateHealth();
    }

    private void UpdateMaxHealh() {
        healthBarSlider.maxValue = PlayerParameters.GetPlayerMaxHealth();
    }

    public void UpdateHealth()
    {
        healthBarSlider.value = PlayerParameters.GetPlayerCurrentHealth();
    }
}

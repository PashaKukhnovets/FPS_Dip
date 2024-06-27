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
        boostBehaviour.updateHealth += UpdateMaxHealth;
        healthBarSlider = this.GetComponent<Slider>();

        healthBarSlider.maxValue = PlayerParameters.GetPlayerMaxHealth();
        healthBarSlider.value = PlayerParameters.GetPlayerCurrentHealth();
    }

    void Update()
    {
        UpdateHealth();
    }

    private void UpdateMaxHealth() {
        healthBarSlider.maxValue = PlayerParameters.GetPlayerMaxHealth();
    }

    public void UpdateHealth()
    {
        healthBarSlider.value = PlayerParameters.GetPlayerCurrentHealth();
    }
}

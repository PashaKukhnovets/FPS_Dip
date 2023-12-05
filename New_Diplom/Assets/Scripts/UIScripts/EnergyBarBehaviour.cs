using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarBehaviour : MonoBehaviour
{
    private Slider energyBarSlider;

    void Start()
    {
        energyBarSlider = this.GetComponent<Slider>();

        energyBarSlider.maxValue = PlayerParameters.GetPlayerMaxEnergy();
        energyBarSlider.value = PlayerParameters.GetPlayerCurrentEnergy();
    }

    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        energyBarSlider.value = PlayerParameters.GetPlayerCurrentEnergy();
    }
}

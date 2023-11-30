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

        energyBarSlider.maxValue = PlayerParameters.GetPlayerEnergy();
        energyBarSlider.value = PlayerParameters.GetPlayerEnergy();
    }

    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        energyBarSlider.value = PlayerParameters.GetPlayerEnergy();
    }
}

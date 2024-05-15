using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarBehaviour : MonoBehaviour
{ 
    [SerializeField] private BoostBehaviour boostBehaviour;

    private Slider energyBarSlider;

    void Start()
    {
        boostBehaviour.updateEnergy += UpdateMaxEnergy;
        energyBarSlider = this.GetComponent<Slider>();

        energyBarSlider.maxValue = PlayerParameters.GetPlayerMaxEnergy();
        energyBarSlider.value = PlayerParameters.GetPlayerCurrentEnergy();
    }

    void Update()
    {
        UpdateEnergy();
    }

    private void UpdateMaxEnergy() {
        energyBarSlider.maxValue = PlayerParameters.GetPlayerMaxEnergy();
    }

    public void UpdateEnergy()
    {
        energyBarSlider.value = PlayerParameters.GetPlayerCurrentEnergy();
    }
}

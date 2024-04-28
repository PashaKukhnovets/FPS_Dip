using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class BoostBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boostHealthText;
    [SerializeField] private TextMeshProUGUI boostEnergyText;
    [SerializeField] private TextMeshProUGUI boostSuperPointsText;

    private int healthLevel = 0;
    private int energyLevel = 0;
    private int superPointsLevel = 0;

    private int healthPrice = 100;
    private int energyPrice = 100;
    private int superPointsPrice = 100;

    public event UnityAction updateHealth;
    public event UnityAction updateEnergy;
    public event UnityAction updatePoints;

    private void Update()
    {
        UpdateBoostText();
    }

    public void BoostHealth() {
        switch (healthLevel) {
            case 0: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 100)
                    {
                        PlayerParameters.AddPlayerMaxHealth(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-100);
                        healthPrice = 300;
                        healthLevel = 1;
                        updateHealth?.Invoke();
                    }
                }
                break;
            case 1: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 300)
                    {
                        PlayerParameters.AddPlayerMaxHealth(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-300);
                        healthLevel = 2;
                        healthPrice = 500;
                        updateHealth?.Invoke();
                    }
                }
                break;
            case 2: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 500)
                    {
                        PlayerParameters.AddPlayerMaxHealth(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-500);
                        healthLevel = 3;
                        updateHealth?.Invoke();
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BoostEnergy() {
        switch (energyLevel)
        {
            case 0:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 100)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-100);
                        energyPrice = 300;
                        energyLevel = 1;
                        updateEnergy?.Invoke();
                    }
                }
                break;
            case 1:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 300)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-300);
                        energyPrice = 500;
                        energyLevel = 2;
                        updateEnergy?.Invoke();
                    }
                }
                break;
            case 2:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 500)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-500);
                        energyLevel = 3;
                        updateEnergy?.Invoke();
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BoostSuperPoints() {
        switch (superPointsLevel)
        {
            case 0:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 100)
                    {
                        PlayerParameters.AddPlayerMaxPoints(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-100);
                        superPointsPrice = 300;
                        superPointsLevel = 1;
                        updatePoints?.Invoke();
                    }
                }
                break;
            case 1:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 300)
                    {
                        PlayerParameters.AddPlayerMaxPoints(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-300);
                        superPointsPrice = 500;
                        superPointsLevel = 2;
                        updatePoints?.Invoke();
                    }
                }
                break;
            case 2:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 500)
                    {
                        PlayerParameters.AddPlayerMaxPoints(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-500);
                        superPointsLevel = 3;
                        updatePoints?.Invoke();
                    }
                }
                break;
            default:
                break;
        }
    }

    private void UpdateBoostText()
    {
        if (healthLevel == 3)
        {
            boostHealthText.text = "Health Upgrade Max";
        }
        else
        {
            boostHealthText.text = "Health Upgrade " + healthPrice.ToString();
        }

        if (energyLevel == 3)
        {
            boostEnergyText.text = "Energy Upgrade Max";
        }
        else
        {
            boostEnergyText.text = "Energy Upgrade " + energyPrice.ToString();
        }

        if (superPointsLevel == 3)
        {
            boostSuperPointsText.text = "Super Points Upgrade Max";
        }
        else
        {
            boostSuperPointsText.text = "Super Points Upgrade " + superPointsPrice.ToString();
        }
    }

    public int GetHealthPrice() {
        return healthPrice;
    }

    public int GetEnergyPrice() {
        return energyPrice;
    }

    public int GetSuperPointsPrice() {
        return superPointsPrice;
    }
}

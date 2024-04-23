using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoostBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI boostHealthText;
    [SerializeField] private TextMeshProUGUI boostEnergyText;
    [SerializeField] private TextMeshProUGUI boostSuperPointsText;

    private int healthLevel = 0;
    private int energyLevel = 0;
    private int superPointsLevel = 0;

    private int healthPrice = 1500;
    private int energyPrice = 1500;
    private int superPointsPrice = 1500;

    private void Update()
    {
        UpdateBoostText();
    }

    public void BoostHealth() {
        Debug.Log("Health");
        switch (healthLevel) {
            case 0: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 1500)
                    {
                        PlayerParameters.AddPlayerMaxHealth(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-1500);
                        healthPrice = 3500;
                        healthLevel = 1;
                    }
                }
                break;
            case 1: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 3500)
                    {
                        PlayerParameters.AddPlayerMaxHealth(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-3500);
                        healthLevel = 2;
                        healthPrice = 5000;
                    }
                }
                break;
            case 2: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 5000)
                    {
                        PlayerParameters.AddPlayerMaxHealth(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-5000);
                        healthLevel = 3;
                        healthPrice = 7000;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BoostEnergy() {
        Debug.Log("Energy");
        switch (energyLevel)
        {
            case 0:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 1500)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-1500);
                        energyPrice = 3500;
                        energyLevel = 1;
                    }
                }
                break;
            case 1:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 3500)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-3500);
                        energyPrice = 5000;
                        energyLevel = 2;
                    }
                }
                break;
            case 2:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 5000)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-5000);
                        energyPrice = 7000;
                        energyLevel = 3;
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BoostSuperPoints() {
        Debug.Log("Superpoints");
        switch (superPointsLevel)
        {
            case 0:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 1500)
                    {
                        PlayerParameters.AddPlayerMaxPoints(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-1500);
                        superPointsPrice = 3500;
                        superPointsLevel = 1;
                    }
                }
                break;
            case 1:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 3500)
                    {
                        PlayerParameters.AddPlayerMaxPoints(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-3500);
                        superPointsPrice = 5000;
                        superPointsLevel = 2;
                    }
                }
                break;
            case 2:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 5000)
                    {
                        PlayerParameters.AddPlayerMaxPoints(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-5000);
                        superPointsPrice = 7000;
                        superPointsLevel = 3;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void UpdateBoostText() {
        boostHealthText.text = "Health Upgrade " + healthPrice.ToString();
        boostEnergyText.text = "Energy Upgrade " + energyPrice.ToString();
        boostSuperPointsText.text = "Super Points Upgrade " + superPointsPrice.ToString();
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

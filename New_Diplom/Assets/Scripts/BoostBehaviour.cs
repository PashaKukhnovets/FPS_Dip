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

    public event UnityAction updateHealth;
    public event UnityAction updateEnergy;
    public event UnityAction updatePoints;

    private void Update()
    {
        UpdateBoostText();
    }

    public void BoostHealth() {
        switch (PlayerParameters.GetHealthLevel()) {
            case 0: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 100)
                    {
                        PlayerParameters.AddPlayerMaxHealth(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-100);
                        PlayerParameters.InitHealthPrice(300);
                        PlayerParameters.InitHealthLevel(1);
                        updateHealth?.Invoke();
                    }
                }
                break;
            case 1: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 300)
                    {
                        PlayerParameters.AddPlayerMaxHealth(30.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-300);
                        PlayerParameters.InitHealthPrice(500);
                        PlayerParameters.InitHealthLevel(2);
                        updateHealth?.Invoke();
                    }
                }
                break;
            case 2: {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 500)
                    {
                        PlayerParameters.AddPlayerMaxHealth(40.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-500);
                        PlayerParameters.InitHealthLevel(3);
                        updateHealth?.Invoke();
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BoostEnergy() {
        switch (PlayerParameters.GetEnergyLevel())
        {
            case 0:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 100)
                    {
                        PlayerParameters.AddPlayerMaxEnergy(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-100);
                        PlayerParameters.InitEnergyPrice(300);
                        PlayerParameters.InitEnergyLevel(1);
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
                        PlayerParameters.InitEnergyPrice(500);
                        PlayerParameters.InitEnergyLevel(2); 
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
                        PlayerParameters.InitEnergyLevel(3); 
                        updateEnergy?.Invoke();
                    }
                }
                break;
            default:
                break;
        }
    }

    public void BoostSuperPoints() {
        switch (PlayerParameters.GetSuperPointsLevel())
        {
            case 0:
                {
                    if (PlayerParameters.GetPlayerCurrentBoostPoints() >= 100)
                    {
                        PlayerParameters.AddPlayerMaxPoints(20.0f);
                        PlayerParameters.AddPlayerCurrentBoostPoints(-100);
                        PlayerParameters.InitSuperPointsPrice(300);
                        PlayerParameters.InitSuperPointsLevel(1);
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
                        PlayerParameters.InitSuperPointsPrice(500);
                        PlayerParameters.InitSuperPointsLevel(2);
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
                        PlayerParameters.InitSuperPointsLevel(3);
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
        if (PlayerParameters.GetHealthLevel() == 3)
        {
            boostHealthText.text = "Health Upgrade Max";
        }
        else
        {
            boostHealthText.text = "Health Upgrade " + PlayerParameters.GetHealthPrice().ToString();
        }

        if (PlayerParameters.GetEnergyLevel() == 3)
        {
            boostEnergyText.text = "Energy Upgrade Max";
        }
        else
        {
            boostEnergyText.text = "Energy Upgrade " + PlayerParameters.GetEnergyPrice().ToString();
        }

        if (PlayerParameters.GetSuperPointsLevel() == 3)
        {
            boostSuperPointsText.text = "Super Points Upgrade Max";
        }
        else
        {
            boostSuperPointsText.text = "Super Points Upgrade " + PlayerParameters.GetSuperPointsPrice().ToString();
        }
    }

    public int GetHealthPrice() {
        return PlayerParameters.GetHealthPrice();
    }

    public int GetEnergyPrice() {
        return PlayerParameters.GetEnergyPrice();
    }

    public int GetSuperPointsPrice() {
        return PlayerParameters.GetSuperPointsPrice();
    }
}

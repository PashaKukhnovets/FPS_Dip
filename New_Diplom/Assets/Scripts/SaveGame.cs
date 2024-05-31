using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private void Start()
    {
        LoadParameters();
    }

    public void SaveParameters() {
        PlayerPrefs.SetInt("healthPrice", PlayerParameters.GetHealthPrice());
        PlayerPrefs.SetInt("energyPrice", PlayerParameters.GetEnergyPrice());
        PlayerPrefs.SetInt("superPointsPrice", PlayerParameters.GetSuperPointsPrice());
        PlayerPrefs.SetInt("healthLevel", PlayerParameters.GetHealthLevel());
        PlayerPrefs.SetInt("energyLevel", PlayerParameters.GetEnergyLevel());
        PlayerPrefs.SetInt("superPointsLevel", PlayerParameters.GetSuperPointsLevel());
        PlayerPrefs.SetInt("currentBoostPoints", PlayerParameters.GetPlayerCurrentBoostPoints());

        PlayerPrefs.SetFloat("maxHealth", PlayerParameters.GetPlayerMaxHealth());
        PlayerPrefs.SetFloat("maxEnergy", PlayerParameters.GetPlayerMaxEnergy());
        PlayerPrefs.SetFloat("maxPoints", PlayerParameters.GetPlayerMaxPoints());
    }

    public void LoadParameters() {
        PlayerParameters.InitHealthPrice(PlayerPrefs.GetInt("healthPrice", PlayerParameters.GetHealthPrice()));
        PlayerParameters.InitEnergyPrice(PlayerPrefs.GetInt("energyPrice", PlayerParameters.GetEnergyPrice()));
        PlayerParameters.InitSuperPointsPrice(PlayerPrefs.GetInt("superPointsPrice", PlayerParameters.GetSuperPointsPrice()));
        PlayerParameters.InitHealthLevel(PlayerPrefs.GetInt("healthLevel", PlayerParameters.GetHealthLevel()));
        PlayerParameters.InitEnergyLevel(PlayerPrefs.GetInt("energyLevel", PlayerParameters.GetEnergyLevel()));
        PlayerParameters.InitSuperPointsLevel(PlayerPrefs.GetInt("superPointsLevel", PlayerParameters.GetSuperPointsLevel()));
        PlayerParameters.InitPlayerCurrentBoostPoints(PlayerPrefs.GetInt("currentBoostPoints", PlayerParameters.GetPlayerCurrentBoostPoints()));

        PlayerParameters.InitMaxHealth(PlayerPrefs.GetFloat("maxHealth", PlayerParameters.GetPlayerMaxHealth()));
        PlayerParameters.InitMaxEnergy(PlayerPrefs.GetFloat("maxEnergy", PlayerParameters.GetPlayerMaxEnergy()));
        PlayerParameters.InitMaxSuperPoints(PlayerPrefs.GetFloat("maxPoints", PlayerParameters.GetPlayerMaxPoints()));
    }
}

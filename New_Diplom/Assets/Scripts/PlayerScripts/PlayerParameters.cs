using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class PlayerParameters
{
    public static float playerDamageAK = 20.0f;
    public static float playerDamagePistol = 16.0f;
    public static float playerDamageShotgun = 35.0f;
    public static float playerDamageGrenade = 50.0f;
    public static float playerDamageKnife = 25.0f;

    private static int healthLevel = 0;
    private static int energyLevel = 0;
    private static int superPointsLevel = 0;

    private static int healthPrice = 100;
    private static int energyPrice = 100;
    private static int superPointsPrice = 100;

    private static float playerMaxHealth = 100.0f;
    private static float playerMaxEnergy = 100.0f;
    private static float playerMaxPoints = 100.0f;

    private static float playerAudioListener = 100.0f;

    private static float playerCurrentHealth;
    private static float playerCurrentEnergy;
    private static float playerCurrentPoints;
    private static int playerCurrentBoostPoints = 0;

    private static bool isWindowOpen = false;

    private static int qualityIndex;

    private static bool isShotgun = false;
    private static bool isAK = false;
    private static bool isGrenade = false;

    public static void InitPlayerCurrentHealth(float health) {
        playerCurrentHealth = health;
    }

    public static void InitPlayerCurrentEnergy(float energy) {
        playerCurrentEnergy = energy;
    }

    public static void InitPlayerCurrentPoints(float points) {
        playerCurrentPoints = points;
    }

    public static void InitPlayerCurrentBoostPoints(int points) {
        playerCurrentBoostPoints = points;
    }

    public static void AddPlayerMaxHealth(float hp) {
        playerMaxHealth += hp;
    }

    public static void AddPlayerMaxEnergy(float energy)
    {
        playerMaxEnergy += energy;
    }

    public static void AddPlayerMaxPoints(float points)
    {
        playerMaxPoints += points;
    }

    public static void AddPlayerDamage(float damage) {
        playerCurrentHealth -= damage;
    }

    public static void AddPlayerCurrentHealth(float health) {
        playerCurrentHealth += health;
    }

    public static void AddPlayerCurrentEnergy(float energy) {
        playerCurrentEnergy += energy;
    }

    public static void AddPlayerCurrentPoints(float points) {
        playerCurrentPoints += points;
    }

    public static void AddPlayerCurrentBoostPoints(int points)
    {
        playerCurrentBoostPoints += points;
    }

    public static void SpendBoostPoints(int points) {
        playerCurrentBoostPoints -= points;
    }

    public static float GetPlayerCurrentHealth() {
        return playerCurrentHealth;
    }

    public static float GetPlayerCurrentEnergy() {
        return playerCurrentEnergy;
    }

    public static float GetPlayerCurrentPoints() {
        return playerCurrentPoints;
    }

    public static int GetPlayerCurrentBoostPoints() {
        return playerCurrentBoostPoints;
    }

    public static float GetPlayerMaxHealth()
    {
        return playerMaxHealth;
    }

    public static float GetPlayerMaxEnergy()
    {
        return playerMaxEnergy;
    }

    public static float GetPlayerMaxPoints()
    {
        return playerMaxPoints;
    }

    public static void SetWindowOpen(bool flag) {
        isWindowOpen = flag;
    }

    public static bool GetWindowOpen() {
        return isWindowOpen;
    }

    public static void SetPlayerAudioListener(float value) {
        playerAudioListener = value;
    }

    public static float GetPlayerAudioListener() {
        return playerAudioListener;
    }

    public static void SetQualityIndex(int value) {
        qualityIndex = value;
    }

    public static int GetQualityIndex()
    {
        return qualityIndex;
    }

    public static void SetShotgun(bool value) {
        isShotgun = value;
    }

    public static void SetAK(bool value)
    {
        isAK = value;
    }

    public static void SetGrenade(bool value)
    {
        isGrenade = value;
    }

    public static bool GetIsShotgun() {
        return isShotgun;
    }

    public static bool GetIsAK() {
        return isAK;
    }

    public static bool GetIsGrenade() {
        return isGrenade;
    }

    public static void InitHealthLevel(int value) {
        healthLevel = value;
    }

    public static void InitEnergyLevel(int value)
    {
        energyLevel = value;
    }

    public static void InitSuperPointsLevel(int value)
    {
        superPointsLevel = value;
    }

    public static void InitHealthPrice(int value)
    {
        healthPrice = value;
    }

    public static void InitEnergyPrice(int value)
    {
        energyPrice = value;
    }

    public static void InitSuperPointsPrice(int value)
    {
        superPointsPrice = value;
    }

    public static int GetHealthLevel()
    {
        return healthLevel;
    }

    public static int GetEnergyLevel()
    {
        return energyLevel;
    }

    public static int GetSuperPointsLevel()
    {
        return superPointsLevel;
    }

    public static int GetHealthPrice()
    {
        return healthPrice;
    }

    public static int GetEnergyPrice()
    {
        return energyPrice;
    }

    public static int GetSuperPointsPrice()
    {
        return superPointsPrice;
    }

    public static void InitMaxHealth(float value) {
        playerMaxHealth = value;
    }

    public static void InitMaxEnergy(float value)
    {
        playerMaxEnergy = value;
    }

    public static void InitMaxSuperPoints(float value)
    {
        playerMaxPoints = value;
    }

}


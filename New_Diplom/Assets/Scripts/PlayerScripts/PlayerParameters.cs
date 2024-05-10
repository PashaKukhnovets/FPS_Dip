using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerParameters
{
    public static float playerDamageAK = 20.0f;
    public static float playerDamagePistol = 10.0f;
    public static float playerDamageShotgun = 35.0f;
    public static float playerDamageGrenade = 50.0f;
    public static float playerDamageKnife = 25.0f;

    private static float playerMaxHealth = 100.0f;
    private static float playerMaxEnergy = 100.0f;
    private static float playerMaxPoints = 100.0f;

    private static float playerAudioListener = 100.0f;

    private static float playerCurrentHealth;
    private static float playerCurrentEnergy;
    private static float playerCurrentPoints;
    private static int playerCurrentBoostPoints = 0;

    private static bool isWindowOpen = false;

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

    public static float GetPlayerCurrentBoostPoints() {
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

}

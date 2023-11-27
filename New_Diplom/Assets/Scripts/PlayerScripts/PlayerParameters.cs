using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerParameters
{
    public static float playerDamage = 20.0f;

    private static float playerHealth = 100.0f;
    private static float playerEnergy = 100.0f;
    private static float playerPoints;

    public static void AddPlayerHealth(float hp) {
        playerHealth += hp;
    }

    public static void AddPlayerDamage(float damage) {
        playerHealth -= damage;
    }

    public static void AddPlayerEnergy(float energy) {
        playerEnergy += energy;
    }

    public static void AddPlayerPoints(float points) {
        playerPoints += points;
    }

    public static float GetPlayerHealth() {
        return playerHealth;
    }

    public static float GetPlayerEnergy() {
        return playerEnergy;
    }

    public static float GetPlayerPoints() {
        return playerPoints;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    public float playerDamage = 10.0f;

    private float playerHealth = 100.0f;
    private float playerEnergy = 100.0f;
    private float playerPoints;

    public void AddPlayerHealth(float hp) {
        this.playerHealth += hp;
    }

    public void AddPlayerDamage(float damage) {
        this.playerHealth -= damage;
    }

    public void AddPlayerEnergy(float energy) {
        this.playerEnergy += energy;
    }

    public void AddPlayerPoints(float points) {
        this.playerPoints += points;
    }

    public float GetPlayerHealth() {
        return this.playerHealth;
    }

    public float GetPlayerEnergy() {
        return this.playerEnergy;
    }

    public float GetPlayerPoints() {
        return this.playerPoints;
    }
}

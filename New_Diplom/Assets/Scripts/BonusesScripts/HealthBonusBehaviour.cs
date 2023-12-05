using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBonusBehaviour : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (PlayerParameters.GetPlayerCurrentHealth() < PlayerParameters.GetPlayerMaxHealth())
            {
                if (PlayerParameters.GetPlayerMaxHealth() - PlayerParameters.GetPlayerCurrentHealth() <= 20.0f)
                {
                    PlayerParameters.InitPlayerCurrentHealth(PlayerParameters.GetPlayerMaxHealth());
                    Destroy(this.gameObject);
                }
                else
                {
                    PlayerParameters.AddPlayerCurrentHealth(20.0f);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

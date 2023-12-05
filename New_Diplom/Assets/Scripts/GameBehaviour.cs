using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletStore;

    private GameObject playerWeapon;

    private void Update()
    {
        UpdateBulletStoreText();
    }

    private void CheckPlayerDeath() {
        if (PlayerParameters.GetPlayerHealth() <= 0.0f) { 
            
        }
    }

    private void UpdateBulletStoreText() {
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");

        if (playerWeapon.GetComponent<AKBehaviour>())
        {
            bulletStore.text = playerWeapon.GetComponent<AKBehaviour>().GetCurrentBulletCount().ToString() + "/" +
                playerWeapon.GetComponent<AKBehaviour>().GetAmountOfBullets().ToString();
        }
        //else if ()
        //{

        //}
    }
}

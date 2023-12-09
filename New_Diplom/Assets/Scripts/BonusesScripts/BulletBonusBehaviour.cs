using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBonusBehaviour : MonoBehaviour
{
    private GameObject playerWeapon;

    void Start()
    {
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (playerWeapon.GetComponent<AKBehaviour>().GetAmountOfBullets() < 120)
            {
                if (120 - playerWeapon.GetComponent<AKBehaviour>().GetAmountOfBullets() <= 30.0f)
                {
                    playerWeapon.GetComponent<AKBehaviour>().InitAmountOfBullets(120);
                    Destroy(this.gameObject);
                }
                else
                {
                    playerWeapon.GetComponent<AKBehaviour>().AddAmountOfBullets(20);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}

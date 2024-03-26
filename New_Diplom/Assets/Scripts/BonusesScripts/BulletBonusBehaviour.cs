using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBonusBehaviour : MonoBehaviour
{
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (gameManager.GetComponent<GameBehaviour>().CheckAK())
            {
                if (gameManager.GetComponent<GameBehaviour>().GetAmountOfAKBullets() < 120)
                {
                    if (120 - gameManager.GetComponent<GameBehaviour>().GetAmountOfAKBullets() <= 20.0f)
                    {
                        gameManager.GetComponent<GameBehaviour>().InitAmountOfAKBullets();
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        gameManager.GetComponent<GameBehaviour>().AddAmountOfAKBullets();
                        Destroy(this.gameObject);
                    }
                }
            }
            else if (gameManager.GetComponent<GameBehaviour>().CheckPistol())
                {
                if (gameManager.GetComponent<GameBehaviour>().GetAmountOfPistolBullets() < 35)
                {
                    if (35 - gameManager.GetComponent<GameBehaviour>().GetAmountOfPistolBullets() <= 10.0f)
                    {
                        gameManager.GetComponent<GameBehaviour>().InitAmountOfPistolBullets();
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        gameManager.GetComponent<GameBehaviour>().AddAmountOfPistolBullets();
                        Destroy(this.gameObject);
                    }
                } 
            }
        }
    }
}

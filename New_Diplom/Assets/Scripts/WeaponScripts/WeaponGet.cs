using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGet : MonoBehaviour
{
    private GameObject gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) {
            if (this.gameObject.CompareTag("AKGet") && !other.gameObject.GetComponent<PlayerController>().GetUseShotgun())
            {
                if (!other.gameObject.GetComponent<PlayerController>().GetUseAK())
                {
                    other.gameObject.GetComponent<PlayerController>().SetUseAK(true);
                }
                else
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
                }
                Destroy(this.gameObject);
            }
            else if (this.gameObject.CompareTag("ShotgunGet") && !other.gameObject.GetComponent<PlayerController>().GetUseAK()) {
                if (!other.gameObject.GetComponent<PlayerController>().GetUseShotgun())
                {
                    other.gameObject.GetComponent<PlayerController>().SetUseShotgun(true);
                }
                else
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
                }
                Destroy(this.gameObject);
            }
        }
    }
}

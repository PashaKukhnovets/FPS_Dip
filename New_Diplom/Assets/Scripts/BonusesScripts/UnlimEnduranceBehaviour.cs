using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlimEnduranceBehaviour : MonoBehaviour
{
    private GameObject player;
    //private GameObject gameManager;
    private float currentDamage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            //gameManager = GameObject.FindGameObjectWithTag("GameManager");
            //gameManager.GetComponent<GameManagerBehaviour>().SetDDTimer(true);
            player.GetComponent<PlayerController>().SetEnergyBonus(true);
            StartCoroutine(UnlimEnduranceCoroutine());
            this.gameObject.transform.position = new Vector3(0.0f, 100.0f, 0.0f);
        }
    }

    private IEnumerator UnlimEnduranceCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<PlayerController>().SetEnergyBonus(false);
        Destroy(this.gameObject);
    }
}

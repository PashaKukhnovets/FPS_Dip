using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperGetBehaviour : MonoBehaviour
{
    private GameBehaviour gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) {
            gameManager.AddCountOfPapers(1);
            Destroy(this.gameObject);
        }
    }
}

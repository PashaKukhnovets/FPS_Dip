using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTerrorist : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrorists;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>()) {
            foreach (var v in terrorists) {
                v.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] terrorists;

    public void TerroristsOn() {
        foreach (var terrorist in terrorists) {
            Debug.Log("spawn");
            terrorist.gameObject.SetActive(true);
        } 
    }
}

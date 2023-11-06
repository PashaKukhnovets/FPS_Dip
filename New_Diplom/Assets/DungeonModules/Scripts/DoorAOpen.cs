using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAOpen : MonoBehaviour
{
    [SerializeField] private GameObject puzzleWire_1;

    void OnTriggerEnter(Collider other)
    {
        GetComponent<Animator>().SetTrigger("DoorATrigger");
        puzzleWire_1.SetActive(true);
    }
}
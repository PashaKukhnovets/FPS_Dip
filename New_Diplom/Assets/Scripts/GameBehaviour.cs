using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject puzzles;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject player;
    
    void Start()
    {
        
    }

    void Update()
    {
        ChangeCameraProjection();
    }

    private void ChangeCameraProjection() {
        if (CheckPuzzleActivity())
        {
            playerCamera.orthographic = true;
            player.GetComponent<MouseLook>().enabled = false;
            playerCamera.GetComponent<MouseLook>().enabled = false;
        }
        else
        {
            playerCamera.orthographic = false;
            player.GetComponent<MouseLook>().enabled = true;
            playerCamera.GetComponent<MouseLook>().enabled = true;
        }
    }

    public bool CheckPuzzleActivity() {
        if (puzzles.activeSelf)
            return true;
        else
            return false;
    }
}

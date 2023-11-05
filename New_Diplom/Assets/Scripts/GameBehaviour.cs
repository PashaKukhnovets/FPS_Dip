using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject puzzles;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWeapon;

    [SerializeField] private CheckWinTube puzzleTube_1;
    
    void Start()
    {
        puzzleTube_1.PuzzleWin += OffPuzzle;
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
            playerCamera.GetComponent<RayShooting>().enabled = false;
            playerWeapon.GetComponent<WeaponAnimationController>().enabled = false;
        }
        else
        {
            playerCamera.orthographic = false;
            player.GetComponent<MouseLook>().enabled = true;
            playerCamera.GetComponent<MouseLook>().enabled = true;
            playerCamera.GetComponent<RayShooting>().enabled = true;
            playerWeapon.GetComponent<WeaponAnimationController>().enabled = true;
        }
    }

    private void OffPuzzle() {
        puzzles.SetActive(false);
    }

    public bool CheckPuzzleActivity() {
        if (puzzles.activeSelf)
            return true;
        else
            return false;
    }
}

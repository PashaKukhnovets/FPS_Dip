using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehaviour : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWeapon;

    [SerializeField] private CheckWinTube puzzleTube_1;
    [SerializeField] private CheckWinWires puzzleWires_1;

    void Start()
    {

    }

    void Update()
    {
        ChangeCameraProjection();
    }

    private void ChangeCameraProjection()
    {
        if (CheckPuzzleActivity())
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            puzzleCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            player.GetComponent<MouseLook>().enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
            playerWeapon.GetComponent<WeaponAnimationController>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            puzzleCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            player.GetComponent<MouseLook>().enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            playerWeapon.GetComponent<WeaponAnimationController>().enabled = true;
        }
    }

    public bool CheckPuzzleActivity()
    {
        if (puzzleTube_1.gameObject.activeSelf || puzzleWires_1.gameObject.activeSelf)
            return true;
        else
            return false;
    }
}
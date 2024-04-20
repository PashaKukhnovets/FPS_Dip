using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PuzzleBehaviour : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerWeapon;

    [SerializeField] private CheckWinTube puzzleTube_1;
    [SerializeField] private CheckWinWires puzzleWires_1;
    [SerializeField] private CheckWinTube puzzleNumbers_1;
    [SerializeField] private CheckWinTube puzzleTube_2;
    [SerializeField] private CheckWinWires puzzleWires_2;
    [SerializeField] private CheckWinTube puzzleNumbers_2;
    [SerializeField] private CheckWinTube puzzleTube_3;
    [SerializeField] private CheckWinWires puzzleWires_3;
    [SerializeField] private CheckWinTube puzzleNumbers_3;
    [SerializeField] private CheckWinTube puzzleTube_4;
    [SerializeField] private CheckWinWires puzzleWires_4;
    [SerializeField] private CheckWinTube puzzleNumbers_4;
    [SerializeField] private CheckWinTube puzzleTube_5;
    [SerializeField] private CheckWinWires puzzleWires_5;
    [SerializeField] private CheckWinTube puzzleNumbers_5;
    [SerializeField] private CheckWinTube puzzleTube_6;
    [SerializeField] private CheckWinWires puzzleWires_6;
    [SerializeField] private CheckWinTube puzzleNumbers_6;

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
            //Cursor.lockState = CursorLockMode.Confined;
            //Cursor.visible = true;
            playerWeapon.GetComponent<RigBuilder>().enabled = false;
            puzzleCamera.gameObject.SetActive(true);
            playerCamera.gameObject.SetActive(false);
            player.GetComponent<PlayerController>().BlockPlayerMove(false);
        }
        else
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            puzzleCamera.gameObject.SetActive(false);
            playerCamera.gameObject.SetActive(true);
            player.GetComponent<PlayerController>().BlockPlayerMove(true);
        }
    }

    public bool CheckPuzzleActivity()
    {
        if (puzzleTube_1.gameObject.activeSelf || puzzleWires_1.gameObject.activeSelf ||
            puzzleTube_2.gameObject.activeSelf || puzzleWires_2.gameObject.activeSelf ||
            puzzleTube_3.gameObject.activeSelf || puzzleWires_3.gameObject.activeSelf ||
            puzzleTube_4.gameObject.activeSelf || puzzleWires_4.gameObject.activeSelf || 
            puzzleTube_5.gameObject.activeSelf || puzzleWires_5.gameObject.activeSelf ||
            puzzleTube_6.gameObject.activeSelf || puzzleWires_6.gameObject.activeSelf ||
            puzzleNumbers_1.gameObject.activeSelf || puzzleNumbers_2.gameObject.activeSelf ||
            puzzleNumbers_3.gameObject.activeSelf || puzzleNumbers_4.gameObject.activeSelf ||
            puzzleNumbers_5.gameObject.activeSelf || puzzleNumbers_6.gameObject.activeSelf)
        {
            return true;
        }
        else
            return false;
    }
}

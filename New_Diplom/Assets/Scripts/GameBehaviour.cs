using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletStore;
    [SerializeField] private TextMeshProUGUI boostPoints;
    [SerializeField] private GameObject boostWindow;
    [SerializeField] private PuzzleBehaviour puzzle;

    private GameObject playerWeapon;
    private bool isBoostOpen = false;
    private bool isCursorShow = false;

    private void Update()
    {
        UpdateBulletStoreText();
        UpdateBoostPointsText();
        BoostWindowVisibility();
        ShowCursor();
    }

    private void CheckPlayerDeath() {
        if (PlayerParameters.GetPlayerCurrentHealth() <= 0.0f) { 
            
        }
    }

    private void UpdateBulletStoreText() {
        if (!puzzle.CheckPuzzleActivity())
        {
            playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");

            if (playerWeapon.GetComponent<AKBehaviour>())
            {
                bulletStore.text = playerWeapon.GetComponent<AKBehaviour>().GetCurrentBulletCount().ToString() + "/" +
                    playerWeapon.GetComponent<AKBehaviour>().GetAmountOfBullets().ToString();
            }
            //else if ()
            //{

            //}
        }
    }

    private void UpdateBoostPointsText() {
        boostPoints.text = PlayerParameters.GetPlayerCurrentBoostPoints().ToString();
    }

    private void BoostWindowVisibility() {
        if (Input.GetKeyDown(KeyCode.Q) && !isBoostOpen)
        {
            boostWindow.gameObject.SetActive(true);
            isBoostOpen = true;
            isCursorShow = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Q)) && isBoostOpen)
        {
            boostWindow.gameObject.SetActive(false);
            isBoostOpen = false;
            isCursorShow = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void ShowCursor() {
        if (isCursorShow && isBoostOpen)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
        //else if (!isCursorShow && !isBoostOpen)
        //{
        //    Cursor.lockState = CursorLockMode.Locked;
        //    Cursor.visible = false;
        //}
    }
}

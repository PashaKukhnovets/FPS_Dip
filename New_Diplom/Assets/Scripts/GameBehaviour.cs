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
    [SerializeField] private GameObject escapeWindow;

    private GameObject playerWeapon;
    private bool isBoostOpen = false;
    private bool isCursorActive = false;

    private void Update()
    {
        UpdateBulletStoreText();
        UpdateBoostPointsText();
        BoostWindowVisibility();
        ShowCursor();
        EscapeWindowVisibility();
        Debug.Log(PlayerParameters.GetWindowOpen());
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
        if (Input.GetKeyDown(KeyCode.Q) && !PlayerParameters.GetWindowOpen())
        {
            boostWindow.gameObject.SetActive(true);
            PlayerParameters.SetWindowOpen(true);
            isBoostOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && PlayerParameters.GetWindowOpen() && isBoostOpen)
        {
            boostWindow.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            PlayerParameters.SetWindowOpen(false);
            isBoostOpen = false;
        }
    }

    private void ShowCursor() {
        if (boostWindow.activeSelf || escapeWindow.activeSelf || puzzle.CheckPuzzleActivity())
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    private void EscapeWindowVisibility()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerParameters.GetWindowOpen())
        {
            escapeWindow.gameObject.SetActive(true);
            PlayerParameters.SetWindowOpen(true);
        }
    }  
}

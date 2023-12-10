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
    [SerializeField] private GameObject deathWindow;

    private GameObject playerWeapon;
    private GameObject player;
    private bool isBoostOpen = false;
    private bool isCursorActive = false;
    private bool isDeath = false;

    private void Start()
    {
        Time.timeScale = 1;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        UpdateBulletStoreText();
        UpdateBoostPointsText();
        BoostWindowVisibility();
        ShowCursor();
        EscapeWindowVisibility();
        CheckPlayerDeath();
        DeathBlock();
    }

    private void CheckPlayerDeath() {
        if (PlayerParameters.GetPlayerCurrentHealth() <= 0.0f && !isDeath) {
            isDeath = true;
            boostWindow.SetActive(false);
            escapeWindow.SetActive(false);
            PlayerParameters.SetWindowOpen(true);
            Time.timeScale = 0;
            deathWindow.SetActive(true);
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
            player.GetComponent<PlayerController>().BlockPlayerMove(true);
        }
    }

    private void ShowCursor() {
        if (boostWindow.activeSelf || escapeWindow.activeSelf || puzzle.CheckPuzzleActivity())
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.GetComponent<PlayerController>().BlockPlayerMove(false);
        }
    }

    private void EscapeWindowVisibility()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerParameters.GetWindowOpen())
        {
            escapeWindow.gameObject.SetActive(true);
            PlayerParameters.SetWindowOpen(true);
            player.GetComponent<PlayerController>().BlockPlayerMove(false);
        }
    }

    private void DeathBlock() {
        if (PlayerParameters.GetPlayerCurrentHealth() <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.GetComponent<PlayerController>().BlockPlayerMove(false);
        }
    }
}

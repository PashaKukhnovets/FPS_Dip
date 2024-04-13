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
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject grenade;

    private GameObject player;
    private bool isBoostOpen = false;
    private bool isCursorActive = false;
    private bool isDeath = false;
    private bool isPistol;
    private bool isAK;
    private bool isShotgun;
    private bool isGrenade;
    private bool isWasPuzzle = false;

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

        if (!isWasPuzzle && puzzle.CheckPuzzleActivity())
        {
            isWasPuzzle = true;
        }
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
            if (pistol.activeSelf)
            {
                bulletStore.text = pistol.GetComponent<PistolBehaviour>().GetCurrentBulletCount().ToString() + "/" +
                    pistol.GetComponent<PistolBehaviour>().GetAmountOfBullets().ToString();
                isPistol = true;
                isAK = false;
                isShotgun = false;
                isGrenade = false;
            }
            else if (ak.activeSelf)
            {
                bulletStore.text = ak.GetComponent<AKBehaviour>().GetCurrentBulletCount().ToString() + "/" +
                    ak.GetComponent<AKBehaviour>().GetAmountOfBullets().ToString();
                isPistol = false;
                isShotgun = false;
                isAK = true;
                isGrenade = false;
            }
            else if (shotgun.activeSelf) {
                bulletStore.text = shotgun.GetComponent<ShotgunBehaviour>().GetCurrentBulletCount().ToString() + "/" +
                    shotgun.GetComponent<ShotgunBehaviour>().GetAmountOfBullets().ToString();
                isPistol = false;
                isAK = false;
                isShotgun = true;
                isGrenade = false;
            }
            else if (grenade.activeSelf)
            {
                bulletStore.text = grenade.GetComponent<GrenadeBehaviour>().GetCurrentGrenadesCount().ToString() + "/" +
                    grenade.GetComponent<GrenadeBehaviour>().GetAmountOfGrenades().ToString();
                isPistol = false;
                isAK = false;
                isShotgun = false;
                isGrenade = true;
            }
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
        else if(!boostWindow.activeSelf && !escapeWindow.activeSelf && !puzzle.CheckPuzzleActivity()) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

    public bool CheckPistol()
    {
        return isPistol;
    }

    public bool CheckAK()
    {
        return isAK;
    }

    public bool CheckShotgun() {
        return isShotgun;
    }

    public bool CheckGrenade() {
        return isGrenade;
    }

    public int GetAmountOfPistolBullets() {
        return pistol.GetComponent<PistolBehaviour>().GetAmountOfBullets();
    }

    public void InitAmountOfPistolBullets()
    {
        pistol.GetComponent<PistolBehaviour>().InitAmountOfBullets(35);
    }

    public void AddAmountOfPistolBullets()
    {
        pistol.GetComponent<PistolBehaviour>().AddAmountOfBullets(10);
    }

    public int GetAmountOfAKBullets()
    {
        return ak.GetComponent<AKBehaviour>().GetAmountOfBullets();
    }

    public void InitAmountOfAKBullets()
    {
        ak.GetComponent<AKBehaviour>().InitAmountOfBullets(120);
    }

    public void AddAmountOfAKBullets()
    {
        ak.GetComponent<AKBehaviour>().AddAmountOfBullets(20);
    }

    public int GetAmountOfShotgunBullets()
    {
        return shotgun.GetComponent<ShotgunBehaviour>().GetAmountOfBullets();
    }

    public void InitAmountOfShotgunBullets()
    {
        shotgun.GetComponent<ShotgunBehaviour>().InitAmountOfBullets(25);
    }

    public void AddAmountOfShotgunBullets()
    {
        shotgun.GetComponent<ShotgunBehaviour>().AddAmountOfBullets(5);
    }

    public int GetAmountOfGrenades()
    {
        return grenade.GetComponent<GrenadeBehaviour>().GetAmountOfGrenades();
    }

    public void InitAmountOfGrenades()
    {
        grenade.GetComponent<GrenadeBehaviour>().InitAmountOfGrenades(3);
    }

    public void AddAmountOfGrenades()
    {
        grenade.GetComponent<GrenadeBehaviour>().AddAmountOfGrenades(1);
    }

    public bool CheckPuzzleActivity() {
        return isWasPuzzle;
    }

    public void SetPuzzleActivity(bool value)
    {
        isWasPuzzle = value;
    }
}

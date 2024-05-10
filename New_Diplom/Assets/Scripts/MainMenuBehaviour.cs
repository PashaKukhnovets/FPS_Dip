using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private GameObject levelWindow;
    [SerializeField] private GameObject mainMenu;

    public void StartNewGame() {
        SceneManager.LoadScene(1);
    }

    public void LevelWindowOpen() {
        mainMenu.SetActive(false);
        levelWindow.SetActive(true);
    }

    public void SettingsWindowOpen() {
        mainMenu.SetActive(false);
        settingsWindow.SetActive(true);
    }

    public void LevelWindowClose() {
        mainMenu.SetActive(true);
        levelWindow.SetActive(false);
    }

    public void SettingsWindowClose() {
        mainMenu.SetActive(true);
        settingsWindow.SetActive(false);
    }

    public void StartFirstUndergroundLevel() {
        SceneManager.LoadScene(1);
    }

    public void StartSecondUndergroundLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void StartFirstBuildingLevel()
    {
        SceneManager.LoadScene(3);
    }

    public void StartSecondBuildingLevel()
    {
        SceneManager.LoadScene(4);
    }

    public void ExitGame() {
        Application.Quit();
    }
}

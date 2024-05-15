using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject settingsWindow;
    [SerializeField] private GameObject helpWindow;
    [SerializeField] private GameObject levelWindow;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private TMP_Dropdown dropdownQuality;

    private void Start()
    {
        QualitySettings.SetQualityLevel(PlayerParameters.GetQualityIndex());
        dropdownQuality.value = PlayerParameters.GetQualityIndex();
    }

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

    public void HelpWindowOpen()
    {
        mainMenu.SetActive(false);
        helpWindow.SetActive(true);
    }

    public void LevelWindowClose() {
        mainMenu.SetActive(true);
        levelWindow.SetActive(false);
    }

    public void SettingsWindowClose() {
        mainMenu.SetActive(true);
        settingsWindow.SetActive(false);
    }

    public void HelpWindowClose()
    {
        mainMenu.SetActive(true);
        helpWindow.SetActive(false);
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

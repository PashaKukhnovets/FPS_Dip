using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWindowBehaviour : MonoBehaviour
{
    public void RestartFirstLevelUnderground()
    {
        PlayerParameters.SetWindowOpen(false);
        SceneManager.LoadScene(1);
    }

    public void RestartSecondLevelUnderground()
    {
        PlayerParameters.SetWindowOpen(false);
        SceneManager.LoadScene(2);
    }

    public void RestartFirstLevelBuilding()
    {
        PlayerParameters.SetWindowOpen(false);
        SceneManager.LoadScene(3);
    }

    public void RestartSecondLevelBuilding()
    {
        PlayerParameters.SetWindowOpen(false);
        SceneManager.LoadScene(4);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

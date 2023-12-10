using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWindowBehaviour : MonoBehaviour
{
    public void Restart()
    {
        PlayerParameters.SetWindowOpen(false);
        SceneManager.LoadScene(1);
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}

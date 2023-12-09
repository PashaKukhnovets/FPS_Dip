using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeWindowBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject escapeWindow;

    public void Resume() {
        PlayerParameters.SetWindowOpen(false);
        escapeWindow.SetActive(false);
    }

    public void Save() { 
        
    }

    public void ExitToMainMenu() {
        SceneManager.LoadScene(0);
    }
}

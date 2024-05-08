using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeWindowBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject escapeWindow;
    [SerializeField] private GameObject settingsWindow;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Resume() {
        PlayerParameters.SetWindowOpen(false);
        player.GetComponent<PlayerController>().BlockPlayerMove(true);
        escapeWindow.SetActive(false);
    }

    public void Save() { 
        
    }

    public void Settings() {
        escapeWindow.SetActive(false);
        settingsWindow.SetActive(true);
    }

    public void ExitToMainMenu() {
        SceneManager.LoadScene(0);
    }
}

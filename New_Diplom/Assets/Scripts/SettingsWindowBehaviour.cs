using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsWindowBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject escapeWindow;
    [SerializeField] private GameObject settingsWindow;

    public void ExitToEscapeWindow() {
        settingsWindow.SetActive(false);
        escapeWindow.SetActive(true);
    }
}

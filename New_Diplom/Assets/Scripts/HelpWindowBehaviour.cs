using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpWindowBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject escapeWindow;
    [SerializeField] private GameObject helpWindow;

    public void WindowHelpClose()
    {
        escapeWindow.SetActive(true);
        helpWindow.SetActive(false);
    }
}

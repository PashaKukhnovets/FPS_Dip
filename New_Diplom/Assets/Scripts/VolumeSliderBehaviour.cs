using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Slider slider;

    private void Update()
    {
        ChangeVolume();
    }

    private void ChangeVolume()
    {
        if (settingsPanel.activeSelf)
           AudioListener.volume = slider.value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperPointsBarBehaviour : MonoBehaviour
{
    private Slider superPointsBarSlider;

    void Start()
    {
        superPointsBarSlider = this.GetComponent<Slider>();

        superPointsBarSlider.maxValue = 100.0f;
        superPointsBarSlider.value = PlayerParameters.GetPlayerPoints();
    }

    void Update()
    {
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        superPointsBarSlider.value = PlayerParameters.GetPlayerPoints();
    }
}

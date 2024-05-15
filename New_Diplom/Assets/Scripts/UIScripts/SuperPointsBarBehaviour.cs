using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperPointsBarBehaviour : MonoBehaviour
{
    [SerializeField] private BoostBehaviour boostBehaviour;

    private Slider superPointsBarSlider;

    void Start()
    {
        boostBehaviour.updatePoints += UpdateMaxPoints;
        superPointsBarSlider = this.GetComponent<Slider>();

        superPointsBarSlider.maxValue = PlayerParameters.GetPlayerMaxPoints();
        superPointsBarSlider.value = PlayerParameters.GetPlayerCurrentPoints();
    }

    void Update()
    {
        UpdateSuperPoints();
    }

    private void UpdateMaxPoints() {
        superPointsBarSlider.maxValue = PlayerParameters.GetPlayerMaxPoints();
    }

    public void UpdateSuperPoints()
    {
        superPointsBarSlider.value = PlayerParameters.GetPlayerCurrentPoints();
    }
}

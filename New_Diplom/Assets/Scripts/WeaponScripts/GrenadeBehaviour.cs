using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    [SerializeField] private AKAnimationController animationController;
    [SerializeField] private ChangeWeaponBehaviour change;
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject grenadePoint;
    [SerializeField] private GameObject grenadeThrowPrefab;
    
    private GameObject grenadeInstance;
    private float grenadeSpeed = 900.0f;
    private int amountOfGrenades = 3;
    private int maxGrenadeStore = 1;
    private int currentGrenadeCount = 0;

    void Start()
    {
        currentGrenadeCount = maxGrenadeStore;
    }

    private void Update()
    {
        RefillGrenadeStore();
        CheckCountOfGrenades();
    }

    private void CheckCountOfGrenades() {
        if (amountOfGrenades == 0 && currentGrenadeCount == 0) {
            player.SetUseGrenade(false);
            change.SetToPistol();
            PlayerParameters.SetGrenade(true);
        }
    }
     
    public void InitAmountOfGrenades(int value)
    {
        this.amountOfGrenades = value;
    }

    public int GetAmountOfGrenades()
    {
        return this.amountOfGrenades;
    }

    public int GetGrenadesStore()
    {
        return this.maxGrenadeStore;
    }

    public int GetCurrentGrenadesCount()
    {
        return this.currentGrenadeCount;
    }

    public void AddAmountOfGrenades(int bullets)
    {
        this.amountOfGrenades += bullets;
    }

    public void AddCurrentGrenadesCount(int bullets)
    {
        this.currentGrenadeCount += bullets;
    }

    public void RefillGrenadeStore()
    {
        if (currentGrenadeCount == 0 && amountOfGrenades > 0)
        {
            currentGrenadeCount++;
            amountOfGrenades--;
        }
    }

    public void GrenadeThrow() {
        grenadeInstance = Instantiate(grenadeThrowPrefab, grenadePoint.transform.position, transform.rotation);
        grenadeInstance.GetComponent<Rigidbody>().AddForce(transform.forward * grenadeSpeed);
        currentGrenadeCount--;
    }

}

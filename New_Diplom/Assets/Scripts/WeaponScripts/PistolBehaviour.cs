using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBehaviour : MonoBehaviour
{
    [SerializeField] private AKAnimationController animationController;

    private int amountOfBullets = 35;
    private int maxBulletStore = 7;
    private int currentBulletCount = 0;

    void Start()
    {
        currentBulletCount = maxBulletStore;
        animationController.RefillAK += RefillBulletStore;
    }

    public void InitAmountOfBullets(int value)
    {
        this.amountOfBullets = value;
    }

    public int GetAmountOfBullets()
    {
        return this.amountOfBullets;
    }

    public int GetBulletStore()
    {
        return this.maxBulletStore;
    }

    public int GetCurrentBulletCount()
    {
        return this.currentBulletCount;
    }

    public void AddAmountOfBullets(int bullets)
    {
        this.amountOfBullets += bullets;
    }

    public void AddCurrentBulletCount(int bullets)
    {
        this.currentBulletCount += bullets;
    }

    public void RefillBulletStore()
    {
        if (currentBulletCount >= 0 && currentBulletCount < 7 && amountOfBullets > 0)
        {
            if (amountOfBullets + currentBulletCount >= 7)
            {
                amountOfBullets -= maxBulletStore - currentBulletCount;
                currentBulletCount = maxBulletStore;
            }
            else if (amountOfBullets + currentBulletCount < 7)
            {
                currentBulletCount += amountOfBullets;
                amountOfBullets = 0;
            }
        }
    }
}
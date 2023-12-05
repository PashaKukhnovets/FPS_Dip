using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKBehaviour : MonoBehaviour
{
    private int amountOfBullets = 120;
    private int maxBulletStore = 30;
    private int currentBulletCount = 0;
   
    void Start()
    {
        currentBulletCount = maxBulletStore;
        this.gameObject.GetComponent<AKAnimationController>().RefillAK += RefillBulletStore;
    }

    void Update()
    {
        
    }

    public int GetAmountOfBullets() { 
        return this.amountOfBullets;
    }

    public int GetBulletStore() {
        return this.maxBulletStore;
    }

    public int GetCurrentBulletCount() {
        return this.currentBulletCount;
    }

    public void AddAmountOfBullets(int bullets) {
        this.amountOfBullets += bullets;
    }

    public void AddCurrentBulletCount(int bullets) {
        this.currentBulletCount += bullets;
    }

    public void RefillBulletStore()
    {
        if (currentBulletCount >= 0 && currentBulletCount < 30 && amountOfBullets > 0)
        {
            if (amountOfBullets + currentBulletCount >= 30)
            {
                amountOfBullets -= maxBulletStore - currentBulletCount;
                currentBulletCount = maxBulletStore;
            }
            else if (amountOfBullets + currentBulletCount < 30)
            {
                currentBulletCount += amountOfBullets;
                amountOfBullets = 0;
            }
        } 
    }
}

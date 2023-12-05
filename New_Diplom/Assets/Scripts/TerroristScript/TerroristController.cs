using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerroristController : MonoBehaviour
{
    private RayShooting playerShoot;
    private GameObject player;
    private bool isFirstMovement = true;
    private bool isTerroristRunning = true;
    private bool isInPlayerRadius = false;
    private bool isFreeze = false;
    private bool isDeath = false;

    public float terroristHealth = 100.0f;
    public float damage = 7.0f;

    public event UnityAction TerroristRunFire;
    public event UnityAction TerroristRunFireFalse;
    public event UnityAction TerroristDeath;

    private void Start()
    {
        playerShoot = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<RayShooting>();
        playerShoot.HitChecker += HitByPlayer;
        player = GameObject.FindGameObjectWithTag("Player");
        player.gameObject.GetComponent<PlayerController>().IsFreezeTime += Freeze;
        player.gameObject.GetComponent<PlayerController>().NoFreezeTime += Unfreeze;
        this.gameObject.GetComponent<Pursue>().enabled = false;
        this.gameObject.GetComponent<Face>().enabled = false;
    }

    void Update()
    {
        CheckTerroristDeath();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Face>().enabled = true;
            isFirstMovement = false;
            isTerroristRunning = false;
            TerroristRunFire?.Invoke();
            isInPlayerRadius = true;

            if (!isFreeze)
                this.gameObject.GetComponent<Agent>().maxSpeed = 1.3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            isTerroristRunning = true;
            TerroristRunFireFalse?.Invoke();
            isInPlayerRadius = false;

            if(!isFreeze)
                this.gameObject.GetComponent<Agent>().maxSpeed = 2.5f;
        }

    }

    private void HitByPlayer() {
        if (isFirstMovement) {
            isFirstMovement = false;
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Face>().enabled = true;
        }

        this.terroristHealth -= PlayerParameters.playerDamage;
        Debug.Log("Popal");
    }

    private void Freeze() {
        this.gameObject.GetComponent<Agent>().maxSpeed = 0.7f;
        this.gameObject.GetComponentInChildren<TerroristRayShooting>().changeRateAttackTerrorist(0.5f);
        this.gameObject.GetComponent<Animator>().speed = 0.1f;
        this.gameObject.GetComponent<Agent>().maxAngularAccel = 1.0f;
        isFreeze = true;
    }

    private void Unfreeze() {
        if (isInPlayerRadius)
        {
            this.gameObject.GetComponent<Agent>().maxSpeed = 1.3f;
        }
        else if (!isInPlayerRadius) {
            this.gameObject.GetComponent<Agent>().maxSpeed = 2.5f;
        }

        this.gameObject.GetComponentInChildren<TerroristRayShooting>().changeRateAttackTerrorist(3.5f);
        this.gameObject.GetComponent<Animator>().speed = 1.0f;
        this.gameObject.GetComponent<Agent>().maxAngularAccel = 190.0f;

        isFreeze = false;
    }

    public void CheckTerroristDeath() {
        if (this.terroristHealth <= 0.0f && !isDeath)
        {
            //if (isZombieCount)
            //    GameManagerBehaviour.zombieDeathCount++;
            //isZombieCount = false;
            isDeath = true;
            TerroristDeath?.Invoke();
            this.gameObject.GetComponent<Pursue>().enabled = false;
            this.gameObject.GetComponent<Face>().enabled = false;
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        if (PlayerParameters.GetPlayerCurrentPoints() < 100.0f) {
            PlayerParameters.AddPlayerCurrentPoints(20.0f);
        }

        yield return new WaitForSeconds(4.0f);
        player.gameObject.GetComponent<PlayerController>().IsFreezeTime -= Freeze;
        player.gameObject.GetComponent<PlayerController>().NoFreezeTime -= Unfreeze;
        Destroy(this.gameObject);
    }

    public bool IsTerroristRunning()
    {
        return this.isTerroristRunning;
    }
}

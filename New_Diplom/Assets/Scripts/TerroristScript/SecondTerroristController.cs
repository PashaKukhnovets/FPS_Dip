using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecondTerroristController : MonoBehaviour
{
    [SerializeField] private SecondTerroristRayShooting shooting;

    private GameObject player;
    private bool isFirstMovement = true;
    private bool isFreeze = false;
    private bool isDeath = false;

    public float terroristHealth = 100.0f;
    public float damage = 15.0f;

    public event UnityAction TerroristSitFire;
    public event UnityAction TerroristSitFireFalse;
    public event UnityAction TerroristDeath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.gameObject.GetComponent<PlayerController>().IsFreezeTime += Freeze;
        player.gameObject.GetComponent<PlayerController>().NoFreezeTime += Unfreeze;
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
            this.gameObject.GetComponent<Face>().enabled = true;
            isFirstMovement = false;
            TerroristSitFire?.Invoke();
            
            if (!isFreeze)
                this.gameObject.GetComponent<Agent>().maxSpeed = 1.3f;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            TerroristSitFireFalse?.Invoke();
        }

    }

    public void HitByPlayer()
    {
        if (isFirstMovement)
        {
            isFirstMovement = false;
            this.gameObject.GetComponent<Face>().enabled = true;
        }

        this.terroristHealth -= PlayerParameters.playerDamage;
    }

    private void Freeze()
    {
        this.gameObject.GetComponent<Agent>().maxSpeed = 0.7f;
        this.gameObject.GetComponentInChildren<SecondTerroristRayShooting>().changeRateAttackTerrorist(0.5f);
        this.gameObject.GetComponent<Animator>().speed = 0.1f;
        this.gameObject.GetComponent<Agent>().maxAngularAccel = 1.0f;
        isFreeze = true;
    }

    private void Unfreeze()
    {
        this.gameObject.GetComponentInChildren<SecondTerroristRayShooting>().changeRateAttackTerrorist(3.5f);
        this.gameObject.GetComponent<Animator>().speed = 1.0f;
        this.gameObject.GetComponent<Agent>().maxAngularAccel = 190.0f;

        isFreeze = false;
    }

    public void CheckTerroristDeath()
    {
        if (this.terroristHealth <= 0.0f && !isDeath)
        {
            isDeath = true;
            TerroristDeath?.Invoke();
            this.gameObject.GetComponent<Face>().enabled = false;
            shooting.enabled = false;
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        if (PlayerParameters.GetPlayerCurrentPoints() < 100.0f)
        {
            PlayerParameters.AddPlayerCurrentPoints(20.0f);
        }

        PlayerParameters.AddPlayerCurrentBoostPoints(Random.Range(30, 50));

        yield return new WaitForSeconds(4.0f);
        player.gameObject.GetComponent<PlayerController>().IsFreezeTime -= Freeze;
        player.gameObject.GetComponent<PlayerController>().NoFreezeTime -= Unfreeze;
        Destroy(this.gameObject);
    }

}

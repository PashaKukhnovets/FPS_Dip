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
        this.gameObject.GetComponent<Pursue>().enabled = false;
        this.gameObject.GetComponent<Face>().enabled = false;
        ;
    }

    void Update()
    {
        if (this.terroristHealth <= 0.0f)
        {
            //if (isZombieCount)
            //    GameManagerBehaviour.zombieDeathCount++;
            //isZombieCount = false;
            TerroristDeath?.Invoke();
            this.gameObject.GetComponent<Pursue>().enabled = false;
            this.gameObject.GetComponent<Face>().enabled = false;
            StartCoroutine(DeathCoroutine());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Face>().enabled = true;
            isFirstMovement = false;
            this.gameObject.GetComponent<Agent>().maxSpeed = 1.3f;
            isTerroristRunning = false;
            TerroristRunFire?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            isTerroristRunning = true;
            TerroristRunFireFalse?.Invoke();
            this.gameObject.GetComponent<Agent>().maxSpeed = 2.5f;
        }

    }

    private void HitByPlayer() {
        if (isFirstMovement) {
            isFirstMovement = false;
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Face>().enabled = true;
        }

        this.terroristHealth -= player.GetComponent<PlayerParameters>().playerDamage;
        Debug.Log("Popal");
    }

    private IEnumerator DeathCoroutine()
    {
        yield return new WaitForSeconds(4.0f);
        Destroy(this.gameObject);
    }

    public bool IsTerroristRunning()
    {
        return this.isTerroristRunning;
    }
}

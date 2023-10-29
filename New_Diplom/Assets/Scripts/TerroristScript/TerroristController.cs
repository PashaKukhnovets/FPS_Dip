using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TerroristController : MonoBehaviour
{
    private GameObject player;
    private bool isTerroristCount = true;
    private bool isTerroristRunning = true;

    public float terroristHealth = 100.0f;
    public float damage = 7.0f;

    public event UnityAction TerroristAttack;
    public event UnityAction TerroristAttackFalse;
    public event UnityAction TerroristDeath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
            isTerroristRunning = false;
            TerroristAttack?.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            this.terroristHealth -= player.GetComponent<PlayerParameters>().playerDamage;
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.GetComponent<PlayerController>())
        {
            isTerroristRunning = true;
            TerroristAttackFalse?.Invoke();
        }

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

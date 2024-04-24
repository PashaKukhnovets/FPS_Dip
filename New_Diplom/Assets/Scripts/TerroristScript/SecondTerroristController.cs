using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SecondTerroristController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponDrop;
    [SerializeField] private SecondTerroristRayShooting shooting;
    [SerializeField] private GameObject knifeHitPoint;
    [SerializeField] private ParticleSystem bloodPrefab;

    private GameObject player;
    private bool isFirstMovement = true;
    private bool isFreeze = false;
    private bool isDeath = false;
    private int isDropWeapon;

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
        isDropWeapon = Random.Range(0, 2);
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

    public void BloodInstanceOfKnifeHit()
    {
        StartCoroutine(BloodKnifeDelay());
    }

    public void HitByPlayer(bool isGrenade, bool isPistol, bool isAK, bool isShotgun, bool isKnife)
    {
        if (isFirstMovement)
        {
            isFirstMovement = false;
            this.gameObject.GetComponent<Face>().enabled = true;
        }

        if (isPistol)
        {
            this.terroristHealth -= PlayerParameters.playerDamagePistol;
        }

        if (isAK)
        {
            this.terroristHealth -= PlayerParameters.playerDamageAK;
        }

        if (isShotgun)
        {
            this.terroristHealth -= PlayerParameters.playerDamageShotgun;
        }

        if (isGrenade)
        {
            this.terroristHealth -= PlayerParameters.playerDamageGrenade;
        }

        if (isKnife)
        {
            this.terroristHealth -= PlayerParameters.playerDamageKnife;
        }
    }

    private void Freeze()
    {
        this.gameObject.GetComponent<Agent>().maxSpeed = 0.7f;
        this.gameObject.GetComponentInChildren<SecondTerroristRayShooting>().changeRateAttackTerrorist(0.1f);
        this.gameObject.GetComponent<Animator>().speed = 0.1f;
        this.gameObject.GetComponent<Agent>().maxAngularAccel = 1.0f;
        isFreeze = true;
    }

    private void Unfreeze()
    {
        this.gameObject.GetComponentInChildren<SecondTerroristRayShooting>().changeRateAttackTerrorist(0.4f);
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

            if (isDropWeapon == 1) {
                Instantiate(weaponDrop[Random.Range(0, weaponDrop.Count)], new Vector3(this.gameObject.transform.position.x, 0.3f, this.gameObject.transform.position.z),
                    Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
            }

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

    private IEnumerator BloodKnifeDelay()
    {
        yield return new WaitForSeconds(0.3f);
        ParticleSystem blood = Instantiate(bloodPrefab, knifeHitPoint.transform);
        blood.Play();
    }
}

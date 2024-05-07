using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ThirdTerroristController : MonoBehaviour
{
    [SerializeField] private List<GameObject> weaponDrop;
    [SerializeField] private GameObject knifeHitPoint;
    [SerializeField] private ParticleSystem bloodPrefab;
    [SerializeField] private List<Transform> Points;
    [SerializeField] private ThirdTerroristAnimController thirdTerroristAnimController;

    private GameObject player;
    private bool isFirstMovement = true;
    private bool isTerroristRunning = false;
    private bool isDeath = false;
    private NavMeshAgent agent;
    private bool isPatroling = true;
    private float maxTerroristHealth;
    private int isDropWeapon;

    public float terroristHealth = 400.0f;
    public float damage = 25.0f;
    public bool isStunned = false;

    public event UnityAction ThirdTerroristDeath;
    public event UnityAction ThirdTerroristStunning;
    public event UnityAction ThirdTerroristStunningFalse;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxTerroristHealth = this.terroristHealth;
        player.gameObject.GetComponent<PlayerController>().IsFreezeTime += Freeze;
        player.gameObject.GetComponent<PlayerController>().NoFreezeTime += Unfreeze;
        this.gameObject.GetComponent<Pursue>().enabled = false;
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 2.7f;
        agent.angularSpeed = 240.0f;

        agent.SetDestination(Points[Random.Range(0, Points.Count)].position);
        isDropWeapon = Random.Range(0, 2);
    }

    void Update()
    {
        CheckTerroristDeath();
        Patrol();
    }

    private void Patrol()
    {
        if (isPatroling)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
                agent.SetDestination(Points[Random.Range(0, Points.Count)].position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            this.gameObject.GetComponent<Pursue>().enabled = true;
            isFirstMovement = false;
            isTerroristRunning = true;
            isPatroling = false;
            agent.speed = 4.5f;
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
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Animator>().SetLookAtPosition(player.transform.position);
            this.isTerroristRunning = true;
            isPatroling = false;

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

        if (!isStunned)
        {
            if (IsInRange(this.terroristHealth, (maxTerroristHealth / 2.0f) - 30.0f, (maxTerroristHealth / 2.0f) + 30.0f))
            {
                isStunned = true;
                ThirdTerroristStunning?.Invoke();
                StartCoroutine(StunCoroutine());
            }
        }
    }

    
    private void Freeze()
    {
        this.gameObject.GetComponent<Pursue>().SetDegreesDeltaRotation(0.5f);
        agent.speed = 0.7f;
        this.gameObject.GetComponent<Animator>().speed = 0.1f;    
    }

    private void Unfreeze()
    {
        this.gameObject.GetComponent<Pursue>().SetDegreesDeltaRotation(2.5f);
        agent.speed = 4.5f;
        this.gameObject.GetComponent<Animator>().speed = 1.0f;
    }

    public void CheckTerroristDeath()
    {
        if (this.terroristHealth <= 0.0f && !isDeath)
        {
            isDeath = true;
            agent.isStopped = true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<Pursue>().enabled = false;
            ThirdTerroristDeath?.Invoke();

            if (isDropWeapon == 1)
            {
                Instantiate(weaponDrop[Random.Range(0, weaponDrop.Count)], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.0f, this.gameObject.transform.position.z),
                        Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
            }

            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        if (PlayerParameters.GetPlayerCurrentPoints() < PlayerParameters.GetPlayerMaxPoints())
        {
            PlayerParameters.AddPlayerCurrentPoints(20.0f);
        }

        PlayerParameters.AddPlayerCurrentBoostPoints(Random.Range(30, 50));
        this.gameObject.GetComponent<Pursue>().SetDegreesDeltaRotation(0.0f);

        yield return new WaitForSeconds(4.0f);
        player.gameObject.GetComponent<PlayerController>().IsFreezeTime -= Freeze;
        player.gameObject.GetComponent<PlayerController>().NoFreezeTime -= Unfreeze;
        Destroy(this.gameObject);
    }

    private IEnumerator StunCoroutine() {
        agent.isStopped = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Pursue>().SetDegreesDeltaRotation(0.0f);

        yield return new WaitForSeconds(10.0f);

        this.gameObject.GetComponent<Pursue>().SetDegreesDeltaRotation(2.5f);
        agent.isStopped = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ThirdTerroristStunningFalse?.Invoke();
        thirdTerroristAnimController.SetWalkSoundVariable(true);
    }

    private IEnumerator BloodKnifeDelay()
    {
        yield return new WaitForSeconds(0.3f);
        ParticleSystem blood = Instantiate(bloodPrefab, knifeHitPoint.transform);
        blood.Play();
    }

    public bool IsTerroristRunning()
    {
        return this.isTerroristRunning;
    }

    public bool IsInRange(float value, float start, float end) {
        if (value >= start && value <= end)
        {
            return true;
        }
        else {
            return false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ThirdTerroristController : MonoBehaviour
{
    [SerializeField] private GameObject AKGet;

    private GameObject player;
    private bool isFirstMovement = true;
    private bool isTerroristRunning = false;
    private bool isDeath = false;
    private List<Transform> Points = new List<Transform>();
    private NavMeshAgent agent;
    private bool isPatroling = true;
    private float maxTerroristHealth;

    public float terroristHealth = 400.0f;
    public float damage = 15.0f;
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

        Transform pointsObject = GameObject.FindGameObjectWithTag("Points").transform;

        foreach (Transform point in pointsObject)
        {
            Points.Add(point);
        }

        agent.SetDestination(Points[Random.Range(0, Points.Count)].position);
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

    public void HitByPlayer(bool isGrenade)
    {
        if (isFirstMovement)
        {
            isFirstMovement = false;
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Animator>().SetLookAtPosition(player.transform.position);
            this.isTerroristRunning = true;
            isPatroling = false;

        }
        
        if (!isGrenade)
        {
            this.terroristHealth -= PlayerParameters.playerDamage;
        }
        else
        {
            this.terroristHealth -= 50.0f;
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
        agent.speed = 0.7f;
        this.gameObject.GetComponent<Animator>().speed = 0.1f;    
    }

    private void Unfreeze()
    {
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
            Instantiate(AKGet, new Vector3(this.gameObject.transform.position.x, 0.3f, this.gameObject.transform.position.z),
                Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
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

    private IEnumerator StunCoroutine() {
        agent.isStopped = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        
        yield return new WaitForSeconds(10.0f);

        agent.isStopped = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        ThirdTerroristStunningFalse?.Invoke();
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

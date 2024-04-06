using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class TerroristController : MonoBehaviour
{
    [SerializeField] private GameObject AKGet;
    [SerializeField] private TerroristRayShooting shooting;

    private GameObject player;
    private bool isFirstMovement = true;
    private bool isTerroristRunning = true;
    private bool isInPlayerRadius = false;
    private bool isFreeze = false;
    private bool isDeath = false;
    private List<Transform> Points = new List<Transform>();
    private NavMeshAgent agent;
    private bool isPatroling = true;

    public float terroristHealth = 100.0f;
    public float damage = 7.0f;

    public event UnityAction TerroristRunFire;
    public event UnityAction TerroristRunFireFalse;
    public event UnityAction TerroristDeath;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (isPatroling) {
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
            isTerroristRunning = false;
            TerroristRunFire?.Invoke();
            isInPlayerRadius = true;
            isPatroling = false;

            if (!isFreeze)
                agent.speed = 1.3f;
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
                agent.speed = 4.8f;
        }

    }

    public void HitByPlayer() {
        if (isFirstMovement) {
            isFirstMovement = false;
            this.gameObject.GetComponent<Pursue>().enabled = true;
            this.gameObject.GetComponent<Animator>().SetLookAtPosition(player.transform.position);
            this.isTerroristRunning = true;
            isPatroling = false;
            
        }

        this.terroristHealth -= PlayerParameters.playerDamage;
    }

    private void Freeze() {
        agent.speed = 0.7f;
        this.gameObject.GetComponentInChildren<TerroristRayShooting>().changeRateAttackTerrorist(0.5f);
        this.gameObject.GetComponent<Animator>().speed = 0.1f;
        isFreeze = true;
    }

    private void Unfreeze() {
        if (isInPlayerRadius)
        {
            agent.speed = 1.3f;
        }
        else if (!isInPlayerRadius) {
            agent.speed = 4.8f;
        }

        this.gameObject.GetComponentInChildren<TerroristRayShooting>().changeRateAttackTerrorist(3.5f);
        this.gameObject.GetComponent<Animator>().speed = 1.0f;
        
        isFreeze = false;
    }

    public void CheckTerroristDeath() {
        if (this.terroristHealth <= 0.0f && !isDeath)
        {
            isDeath = true;
            shooting.enabled = false;
            agent.isStopped =true;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            this.gameObject.GetComponent<Pursue>().enabled = false;
            TerroristDeath?.Invoke();
            Instantiate(AKGet, new Vector3(this.gameObject.transform.position.x, 0.3f, this.gameObject.transform.position.z),
                Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
            StartCoroutine(DeathCoroutine());
        }
    }

    private IEnumerator DeathCoroutine()
    {
        if (PlayerParameters.GetPlayerCurrentPoints() < 100.0f) {
            PlayerParameters.AddPlayerCurrentPoints(20.0f);
        }

        PlayerParameters.AddPlayerCurrentBoostPoints(Random.Range(30, 50));

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

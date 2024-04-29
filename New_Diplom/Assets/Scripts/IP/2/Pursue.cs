using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Pursue : MonoBehaviour
{
    private bool isLowDistance = false;
    private NavMeshAgent agent;
    private Animator animator;
    private GameObject player;
    private Vector3 direction;
    private float maxDegreesDelta = 2.5f;
   
    public event UnityAction TerroristRunning;
    public event UnityAction TerroristStandFire;
    public event UnityAction TerroristStandFireFalse;

    public event UnityAction ThirdTerroristAttacking;
    public event UnityAction ThirdTerroristAttackingFalse;
    public event UnityAction ThirdTerroristAgrWalking;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        //agent.stoppingDistance = 7.0f;
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent.SetDestination(player.transform.position);

        if (this.gameObject.GetComponent<TerroristController>()) {
            agent.updateRotation = false;
        }
    }

    private void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        direction = (player.transform.position - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), maxDegreesDelta);

        
        //animator.SetLookAtPosition(player.transform.position);
        if (this.gameObject.GetComponent<TerroristController>())
        {
            if (this.gameObject.GetComponent<TerroristController>().IsTerroristRunning())
            {
                TerroristRunning?.Invoke();
            }
            
            if (Vector3.Distance(agent.gameObject.GetComponent<Transform>().position, player.transform.position) <= 4.0f)
            {
                if (!isLowDistance)
                    isLowDistance = true;

                agent.speed = 0.01f;
                this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                TerroristStandFire?.Invoke();
            }
            else if (Vector3.Distance(agent.gameObject.GetComponent<Transform>().position, player.transform.position) > 4.0f)
            {
                if (isLowDistance)
                {
                    isLowDistance = false;
                    agent.speed = 1.3f;
                    TerroristStandFireFalse?.Invoke();
                    this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                agent.SetDestination(player.transform.position);
            }
        }
        else if (this.gameObject.GetComponent<ThirdTerroristController>()) {
            if (this.gameObject.GetComponent<ThirdTerroristController>().IsTerroristRunning())
            {
                ThirdTerroristAgrWalking?.Invoke();
            }

            if (Vector3.Distance(agent.gameObject.GetComponent<Transform>().position, player.transform.position) <= 4.0f)
            {
                if (!isLowDistance)
                    isLowDistance = true;

                agent.speed = 0.1f;
                ThirdTerroristAttacking?.Invoke();
            }
            else if (Vector3.Distance(agent.gameObject.GetComponent<Transform>().position, player.transform.position) > 4.0f)
            {
                if (isLowDistance)
                {
                    isLowDistance = false;
                    agent.speed = 4.5f;
                    ThirdTerroristAttackingFalse?.Invoke();
                }
                agent.SetDestination(player.transform.position);
            }
        }
    }

    public void SetDegreesDeltaRotation(float value) {
        maxDegreesDelta = value;
    }
}

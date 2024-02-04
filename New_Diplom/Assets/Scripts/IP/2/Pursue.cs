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
    private float distance;

    public float shootDistance = 10.5f; 
   
    public event UnityAction TerroristRunning;
    public event UnityAction TerroristStandFire;
    public event UnityAction TerroristStandFireFalse;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 7.0f;
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        CheckDistance();
    }

    public void CheckDistance()
    {
        agent.SetDestination(player.transform.position);
        animator.SetLookAtPosition(player.transform.position);

        if (this.gameObject.GetComponent<TerroristController>().IsTerroristRunning())
        {
            Debug.Log("run");
            TerroristRunning?.Invoke();
            
        }

        if (agent.remainingDistance <= agent.stoppingDistance + 1.5f)
        {
            if (!isLowDistance)
                isLowDistance = true;

            agent.speed = 0.0f;

            TerroristStandFire?.Invoke();
        }
        else if (agent.remainingDistance > agent.stoppingDistance + 1.5f)
        {
            if (isLowDistance)
            {
                isLowDistance = false;
                agent.speed = 4.8f;
                TerroristStandFireFalse?.Invoke();
            }
        }
    }

    //проблема в том, что терик доходит до цели, и перестает двигаться
}

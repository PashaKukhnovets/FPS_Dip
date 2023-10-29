using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pursue : Seek
{
    public float maxPrediction;
    private GameObject targetAux;
    private Agent targetAgent;
    private bool isLowDistance = false;
    public float targetRadius;
    public float slowRadius;
    public float timeToTarget = 0.1f;

    public event UnityAction TerroristRunning;
    public event UnityAction TerroristStandFire;
    public event UnityAction TerroristStandFireFalse;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
    }

    public override Steering GetSteering()
    {
        if (this.gameObject.GetComponent<TerroristController>().IsTerroristRunning())
        {
            TerroristRunning?.Invoke();
        }

        Steering steering = new Steering();
        Vector3 direction = target.transform.position - transform.position;
        float distance = Mathf.Abs(direction.magnitude);
        float targetSpeed;
        float speed = targetAgent.velocity.magnitude;
        float prediction;

        if (distance < targetRadius)
        {
            if (!isLowDistance)
                isLowDistance = true;

            TerroristStandFire?.Invoke();
            steering.linear = Vector3.zero;
            steering.angular = 0.0f;
            return steering;
        }
        else {
            if (isLowDistance) {
                isLowDistance = false;
                TerroristStandFireFalse?.Invoke();
            }
        }

        if (distance > slowRadius)
            targetSpeed = targetAgent.maxSpeed;
        else
            targetSpeed = targetAgent.maxSpeed * distance / slowRadius;

        Vector3 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linear = desiredVelocity - targetAgent.velocity;
        steering.linear /= timeToTarget;

        if (steering.linear.magnitude > targetAgent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= targetAgent.maxAccel;
        }

        return base.GetSteering();
    }
}

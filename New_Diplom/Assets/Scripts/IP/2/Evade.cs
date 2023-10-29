using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evade : Flee
{
    public float maxPrediction;
    public float escapeRadius;
    public float dangerRadius;
    public float destroyDistance = 0.5f;
    public float timeToTarget = 0.1f;

    private GameObject targetAux;
    private Agent targetAgent;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
    }

    private void OnDestroy()
    {
        Destroy(targetAux);
    }

    public override Steering GetSteering()
    {
        Vector3 direction = transform.position - targetAux.transform.position;
        float distance = direction.magnitude;
        float speed = targetAgent.velocity.magnitude;
        float prediction;
        Steering steering = new Steering();
        float reduce;

        if (distance < destroyDistance)
            this.gameObject.SetActive(false);

        if (distance > dangerRadius)
            return steering;
        if (distance < escapeRadius)
            reduce = 0.0f;
        else
            reduce = distance / dangerRadius * targetAgent.maxSpeed;

        float targetSpeed = targetAgent.maxSpeed - reduce;

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

        //if (distance >= safeDistance) {
        //    Steering steering = new Steering();
        //    steering.linear = Vector3.zero;
        //    return steering; 
        //}

        return base.GetSteering();
    }
}

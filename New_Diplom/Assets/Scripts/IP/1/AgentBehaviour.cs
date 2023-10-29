using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentBehaviour : MonoBehaviour
{
    public GameObject target;
    protected Agent agent;
    public float weight = 1.0f;
    public int priority = 1;

    public virtual void Awake() {
        target = GameObject.FindGameObjectWithTag("Player");
        agent = gameObject.GetComponent<Agent>();
    }

    public virtual void Update() {
        agent.SetSteering(GetSteering(), weight);
        agent.SetSteering(GetSteering(), priority);
    }

    public virtual Steering GetSteering() {
        return new Steering();
    }

    public float MapToRange(float rotation) {
        rotation %= 360.0f;

        if (Mathf.Abs(rotation) > 180.0f) {
            if (rotation < 0.0f)
                rotation += 360.0f;
            else if (rotation > 360.0f)
                rotation -= 360.0f;
        }

        return rotation;
    }

    public Vector3 GetOriAsVec(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
        return vector.normalized;
    }
}

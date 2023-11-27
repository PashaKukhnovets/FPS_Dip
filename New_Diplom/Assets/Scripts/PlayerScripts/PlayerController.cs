using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;

    public float gravity = -9.8f;

    private CharacterController characterController;
    private float rate = 7.0f;
    private float refillRate = 5.0f;
    private float nextStep = 0.0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMove();
        Sprint();
        RefillEnergy();
    }

    private void PlayerMove() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    private void Sprint() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (PlayerParameters.GetPlayerEnergy() > 0.0f)
            {
                if (Time.time > nextStep)
                {
                    nextStep = Time.time + 1.0f / rate;

                    PlayerParameters.AddPlayerEnergy(-5.0f);
                    Debug.Log(PlayerParameters.GetPlayerEnergy());
                }

                this.speed = 8.0f;
            }
            else
                this.speed = 6.0f;
        }
        else
            this.speed = 6.0f;
    }

    private void RefillEnergy() {
        if (!Input.GetKey(KeyCode.LeftShift)) {
            if (PlayerParameters.GetPlayerEnergy() < 100.0f)
            {
                if (Time.time > nextStep)
                {
                    nextStep = Time.time + 1.0f / refillRate;

                    PlayerParameters.AddPlayerEnergy(2.0f);
                    Debug.Log(PlayerParameters.GetPlayerEnergy());
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0f;

    public float gravity = -9.8f;

    private CharacterController characterController;
    private float rate = 7.0f;
    private float refillRate = 5.0f;
    private float nextStep = 0.0f;
    private float jumpForce = 15.0f;
    private float vertSpeed = 0.0f;
    private float termVelocity = -10.0f;
    private bool isFreezing = false;
    private Vector3 movement;
    private bool isEnergyBonus = false;

    public event UnityAction IsFreezeTime;
    public event UnityAction NoFreezeTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        PlayerParameters.InitPlayerCurrentHealth(PlayerParameters.GetPlayerMaxHealth());
        PlayerParameters.InitPlayerCurrentEnergy(PlayerParameters.GetPlayerMaxEnergy());
        PlayerParameters.InitPlayerCurrentPoints(PlayerParameters.GetPlayerMaxPoints());
    }

    void Update()
    {
        PlayerMove();
        Sprint();
        RefillEnergy();
        FreezeTime();
        MinusFreezePoints();
    }

    private void PlayerMove() {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            vertSpeed = jumpForce;
        }
        else
        {
            vertSpeed += gravity * 5.0f * Time.deltaTime;

            if (vertSpeed < termVelocity) {
                vertSpeed = termVelocity;
            }
            
        }

        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        characterController.Move(movement);
    }

    private void Sprint() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (PlayerParameters.GetPlayerCurrentEnergy() > 0.0f)
            {
                if (Time.time > nextStep)
                {
                    nextStep = Time.time + 1.0f / rate;
                    if(!isEnergyBonus)
                        PlayerParameters.AddPlayerCurrentEnergy(-5.0f);
                }

                this.speed = 8.0f;
            }
            else
                this.speed = 6.0f;
        }
        else
            this.speed = 6.0f;
    }

    private void FreezeTime() {
        if (Input.GetKey(KeyCode.Mouse1) && PlayerParameters.GetPlayerCurrentPoints() > 0.0f)
        {
            if (!isFreezing)
            {
                IsFreezeTime?.Invoke();
                isFreezing = true;
            }
        }
        else {
            if (isFreezing)
            {
                NoFreezeTime?.Invoke();
                isFreezing = false;
            }
        }
    }

    private void MinusFreezePoints() {
        if (isFreezing)
        {
            if (Time.time > nextStep)
            {
                nextStep = Time.time + 1.0f / refillRate;
                PlayerParameters.AddPlayerCurrentPoints(-1.0f);
            }
        }
    }

    private void RefillEnergy() {
        if (!Input.GetKey(KeyCode.LeftShift)) {
            if (PlayerParameters.GetPlayerCurrentEnergy() < 100.0f)
            {
                if (Time.time > nextStep)
                {
                    nextStep = Time.time + 1.0f / refillRate;

                    PlayerParameters.AddPlayerCurrentEnergy(2.0f);
                }
            }
        }
    }

    public void SetEnergyBonus(bool value) {
        isEnergyBonus = value;
    }

}

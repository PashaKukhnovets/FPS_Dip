using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject droppingAK;
    [SerializeField] private GameObject droppingShotgun;
    [SerializeField] private ChangeWeaponBehaviour changeWeapon;

    public float speed = 6.0f;

    public float gravity = -9.8f;

    private CharacterController characterController;
    private float rate = 7.0f;
    private float refillRate = 5.0f;
    private float nextStepEnergy = 0.0f;
    private float nextStepPoints = 0.0f;
    private float jumpForce = 11.0f;
    private float vertSpeed = 0.0f;
    private float termVelocity = -10.0f;
    private bool isFreezing = false;
    private Vector3 movement;
    private bool isEnergyBonus = false;
    private GameObject playerWeapon;
    private bool useAK = false;
    private bool useShotgun = false;
    private bool useGrenade = false;

    public event UnityAction IsFreezeTime;
    public event UnityAction NoFreezeTime;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerWeapon = GameObject.FindGameObjectWithTag("PlayerWeapon");

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
        DropWeapon();
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
                if (Time.time > nextStepEnergy)
                {
                    nextStepEnergy = Time.time + 1.0f / rate;
                    if (!isEnergyBonus)
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
        if (Input.GetMouseButton(1) && PlayerParameters.GetPlayerCurrentPoints() > 0.0f)
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
            if (Time.time > nextStepPoints)
            {
                nextStepPoints = Time.time + 1.0f / refillRate;
                PlayerParameters.AddPlayerCurrentPoints(-1.0f);
            }
        }
    }

    private void RefillEnergy() {
        if (!Input.GetKey(KeyCode.LeftShift)) {
            if (PlayerParameters.GetPlayerCurrentEnergy() < 100.0f)
            {
                if (Time.time > nextStepEnergy)
                {
                    nextStepEnergy = Time.time + 1.0f / refillRate;

                    PlayerParameters.AddPlayerCurrentEnergy(2.0f);
                }
            }
        }
    }

    private void DropWeapon()
    {
        if (Input.GetKeyDown(KeyCode.G)) {
            if (GetUseAK())
            {
                SetUseAK(false);
                Instantiate(droppingAK, new Vector3(this.gameObject.transform.position.x + 3 * this.gameObject.transform.forward.x, 0.3f,
                    this.gameObject.transform.position.z + 3 * this.gameObject.transform.forward.z), Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
                changeWeapon.SetToPistol();
            }
            else if (GetUseShotgun()) {
                SetUseShotgun(false);
                Instantiate(droppingShotgun, new Vector3(this.gameObject.transform.position.x + 3 * this.gameObject.transform.forward.x, 0.3f,
                    this.gameObject.transform.position.z + 3 * this.gameObject.transform.forward.z), Quaternion.Euler(new Vector3(0.0f, 0.0f, 180.0f)));
                changeWeapon.SetToPistol();
            }
        }
    }

    public void SetEnergyBonus(bool value) {
        isEnergyBonus = value;
    }

    public void BlockPlayerMove(bool value) {
        this.gameObject.GetComponent<MouseLook>().enabled = value;
        this.gameObject.GetComponent<PlayerController>().enabled = value;
        playerWeapon.GetComponent<AKAnimationController>().enabled = value;
        mainCamera.GetComponent<MouseLook>().enabled = value;
        mainCamera.GetComponent<RayShooting>().enabled = value;
    }

    public void SetUseAK(bool value) {
        this.useAK = value;
    }

    public bool GetUseAK() {
        return this.useAK;
    }

    public void SetUseShotgun(bool value) {
        this.useShotgun = value;
    }

    public bool GetUseShotgun() {
        return this.useShotgun;
    }

    public void SetUseGrenade(bool value)
    {
        this.useGrenade = value;
    }

    public bool GetUseGrenade()
    {
        return this.useGrenade;
    }
}

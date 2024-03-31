using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristRayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private TerroristController terrorist;
    [SerializeField] private GameObject pointShooting;

    private GameObject player;
    private float nextShoot = 0.0f;
    private int countOfShooting = 0;
    private bool isResetCount = true;
    private bool isStartShooting = false;
    private bool isShooting = false;
    private Vector3 temporaryPlayerPosition;
    private float checkPeriod = 2.5f;
    private float lastCheckPeriod;

    public float terroristRate = 3.5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        terrorist.TerroristRunFire += ShootPositive;
        terrorist.TerroristRunFireFalse += ShootNegative;
    }

    private void Update()
    {
        Shoot();
        PlayerAIM();
        SetTemporaryPlayerPosition();
    }

    private void Shoot()
    {
        if (countOfShooting <= 5)
        {
            if (Time.time > nextShoot && isShooting)
            {
                countOfShooting++;

                nextShoot = Time.time + 1.0f / terroristRate;

                muzzleEffect.Play();

                RaycastHit hit;
                if (Physics.SphereCast(pointShooting.transform.position, 1.0f, temporaryPlayerPosition - pointShooting.transform.position, out hit, 5000.0f))
                {
                    GameObject hitObject = hit.transform.gameObject;

                    if (hitObject.GetComponent<PlayerController>())
                    {
                        PlayerParameters.AddPlayerDamage(15.0f);
                        StartCoroutine(BloodEffect(hit));
                    }
                }
            }
        }
        else {
            if (isResetCount)
            {
                StartCoroutine(ResetCountOfShooting());
                isResetCount = false;
            }
        }
    }

    private void PlayerAIM() {
        if (isStartShooting && !isShooting)
        {
            RaycastHit hit;
            if (Physics.SphereCast(pointShooting.transform.position, 1.0f, temporaryPlayerPosition - pointShooting.transform.position, out hit, 5000.0f))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerController>())
                {
                    isShooting = true;
                }
            }
        }
    }

    private void ShootPositive() {
        StartCoroutine(TerroristFirstShoot());
    }

    private void ShootNegative() {
        isStartShooting = false;
    }

    private void SetTemporaryPlayerPosition()
    {
        lastCheckPeriod -= Time.deltaTime;
        if (lastCheckPeriod < 0.0f)
        {
            temporaryPlayerPosition = player.transform.position;
            lastCheckPeriod = checkPeriod;
        }
    }

    private IEnumerator ResetCountOfShooting()
    { 
        yield return new WaitForSeconds(3);

        countOfShooting = 0;
        isResetCount = true;
    }

    private IEnumerator BloodEffect(RaycastHit hit)
    {
        GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

        yield return new WaitForSeconds(1.5f);

        Destroy(blood);
    }

    private IEnumerator TerroristFirstShoot() {
        yield return new WaitForSeconds(1.5f);

        isStartShooting = true;
    }

    public void changeRateAttackTerrorist(float currentRate) {
        this.terroristRate = currentRate;
    }

}

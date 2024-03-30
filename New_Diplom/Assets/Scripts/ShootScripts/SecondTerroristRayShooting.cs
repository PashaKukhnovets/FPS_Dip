using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTerroristRayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private SecondTerroristController terrorist;
    [SerializeField] private GameObject pointShooting;

    private GameObject player;
    private Vector3 temporaryPlayerPosition;
    private float nextShoot = 0.0f;
    private int countOfShooting = 0;
    private bool isResetCount = true;
    private bool isStartShooting = false;
    private bool isShooting = false;
    private bool isFirstShoot = true;

    private float checkPeriod = 1.0f;
    private float lastCheckPeriod;
    public float terroristRate = 3.5f;

    private void Start()
    {
        lastCheckPeriod = checkPeriod;
        player = GameObject.FindGameObjectWithTag("Player");
        terrorist.TerroristSitFire += ShootPositive;
        terrorist.TerroristSitFireFalse += ShootNegative;
    }

    private void Update()
    {
        Shoot();
        PlayerAIM();
        SetTemporaryPlayerPosition();
    }

    private void Shoot()
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

                if (hitObject.CompareTag("Player"))
                {
                    PlayerParameters.AddPlayerDamage(15.0f);
                    StartCoroutine(BloodEffect(hit));
                }
            }
        }
    }

    private void PlayerAIM()
    {
        if (isStartShooting && !isShooting)
        {
            RaycastHit hit;
            if (Physics.SphereCast(pointShooting.transform.position, 1.0f, temporaryPlayerPosition - pointShooting.transform.position, out hit, 5000.0f))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.CompareTag("Player"))
                {
                    isShooting = true;
                }
            }
        }
    }

    private void ShootPositive()
    {
        if (isFirstShoot)
        {
            isFirstShoot = false;

            StartCoroutine(TerroristFirstShoot());
        }
        else
            isStartShooting = true;
    }

    private void ShootNegative()
    {
        isStartShooting = false;
    }

    private void SetTemporaryPlayerPosition()
    {
        lastCheckPeriod -= Time.deltaTime;
        if (lastCheckPeriod < 0.0f) {
            temporaryPlayerPosition = player.transform.position;
            lastCheckPeriod = checkPeriod;
        }
    }

    private IEnumerator BloodEffect(RaycastHit hit)
    {
        GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

        yield return new WaitForSeconds(1.5f);

        Destroy(blood);
    }

    private IEnumerator TerroristFirstShoot()
    {
        yield return new WaitForSeconds(3.5f);

        isStartShooting = true;
    }

    public void changeRateAttackTerrorist(float currentRate)
    {
        this.terroristRate = currentRate;
    }
}

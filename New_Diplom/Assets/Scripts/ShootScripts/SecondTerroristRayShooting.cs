using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTerroristRayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private SecondTerroristController terrorist;
    [SerializeField] private GameObject pointShooting;

    private float nextShoot = 0.0f;
    private int countOfShooting = 0;
    private bool isResetCount = true;
    private bool isStartShooting = false;
    private bool isShooting = false;
    private bool isFirstShoot = true;

    public float terroristRate = 3.5f;

    private void Start()
    {
        terrorist.TerroristSitFire += ShootPositive;
        terrorist.TerroristSitFireFalse += ShootNegative;
    }

    private void Update()
    {
        Shoot();
        PlayerAIM();
    }

    private void Shoot()
    {
        if (Time.time > nextShoot && isShooting)
        {
            countOfShooting++;

            nextShoot = Time.time + 1.0f / terroristRate;

            muzzleEffect.Play();

            Ray ray = new Ray(pointShooting.transform.position, pointShooting.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
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

    private void PlayerAIM()
    {
        if (isStartShooting && !isShooting)
        {
            Ray ray = new Ray(pointShooting.transform.position, pointShooting.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<PlayerController>())
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
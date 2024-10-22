using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem akMuzzleEffect;
    [SerializeField] private ParticleSystem pistolMuzzleEffect;
    [SerializeField] private ParticleSystem shotgunMuzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private AKAnimationController weaponAnim;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject grenade;

    [SerializeField] private AudioSource akSound;
    [SerializeField] private AudioSource pistolSound;
    [SerializeField] private AudioSource shotgunSound;

    private float rate = 7.0f;
    private float nextShoot = 0.0f;

    void OnGUI()
    {
        int size = 20;
        float posX = playerCamera.pixelWidth / 2 - size / 4;
        float posY = playerCamera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+");
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (ak.activeSelf){
            if (Input.GetButton("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
               ak.GetComponent<AKBehaviour>().GetCurrentBulletCount() > 0)
            {
                akMuzzleEffect.Play();
                nextShoot = Time.time + 1.0f / rate;
                ak.GetComponent<AKBehaviour>().AddCurrentBulletCount(-1);
                PlayerRayCast(false);
                akSound.Play();
            }
        }
        else if (pistol.activeSelf){
            if (Input.GetButtonDown("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
               pistol.GetComponent<PistolBehaviour>().GetCurrentBulletCount() > 0)
            {
                pistolMuzzleEffect.Play();
                nextShoot = Time.time + 1.0f / rate;
                pistol.GetComponent<PistolBehaviour>().AddCurrentBulletCount(-1);
                PlayerRayCast(false);
                pistolSound.Play();
            }
        }
        else if (shotgun.activeSelf)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
               shotgun.GetComponent<ShotgunBehaviour>().GetCurrentBulletCount() > 0)
            {
                shotgunMuzzleEffect.Play();
                nextShoot = Time.time + 1.0f / 1.5f;
                shotgun.GetComponent<ShotgunBehaviour>().AddCurrentBulletCount(-1);
                PlayerRayCast(true);
                shotgunSound.Play();
            }
        }

    }

    private void PlayerRayCast(bool isShotgun) {

        if (!isShotgun)
        {
            mouseLook.ChangeOffsetRecoil(Random.Range(0.0f, 1.7f), Random.Range(-1.9f, 1.9f));
            Vector3 point = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
            Ray ray = playerCamera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.gameObject.CompareTag("Terrorist"))
                {
                    if (hitObject.gameObject.GetComponent<TerroristController>())
                    {
                        hitObject.GetComponent<TerroristController>().HitByPlayer(grenade.activeSelf, pistol.activeSelf, ak.activeSelf, shotgun.activeSelf, false);
                        StartCoroutine(BloodEffect(hit));
                    }
                    if (hitObject.gameObject.GetComponent<SecondTerroristController>())
                    {
                        hitObject.GetComponent<SecondTerroristController>().HitByPlayer(grenade.activeSelf, pistol.activeSelf, ak.activeSelf, shotgun.activeSelf, false);
                        StartCoroutine(BloodEffect(hit));
                    }
                    if (hitObject.gameObject.GetComponent<ThirdTerroristController>())
                    {
                        hitObject.GetComponent<ThirdTerroristController>().HitByPlayer(grenade.activeSelf, pistol.activeSelf, ak.activeSelf, shotgun.activeSelf, false);
                        StartCoroutine(BloodEffect(hit));
                    }
                }
            }
        }
        else {
            mouseLook.ChangeOffsetRecoil(Random.Range(0.0f, 7.0f), Random.Range(-7.0f, 7.0f));
            Vector3 point = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
            Ray ray = playerCamera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 1.0f, out hit, 15.0f))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.gameObject.CompareTag("Terrorist"))
                {
                    if (hitObject.gameObject.GetComponent<TerroristController>())
                    {
                        hitObject.GetComponent<TerroristController>().HitByPlayer(grenade.activeSelf, pistol.activeSelf, ak.activeSelf, shotgun.activeSelf, false);
                        StartCoroutine(BloodEffect(hit));
                    }
                    if (hitObject.gameObject.GetComponent<SecondTerroristController>())
                    {
                        hitObject.GetComponent<SecondTerroristController>().HitByPlayer(grenade.activeSelf, pistol.activeSelf, ak.activeSelf, shotgun.activeSelf, false);
                        StartCoroutine(BloodEffect(hit));
                    }
                    if (hitObject.gameObject.GetComponent<ThirdTerroristController>())
                    {
                        hitObject.GetComponent<ThirdTerroristController>().HitByPlayer(grenade.activeSelf, pistol.activeSelf, ak.activeSelf, shotgun.activeSelf, false);
                        StartCoroutine(BloodEffect(hit));
                    }
                }
            }
        }
    }

    private IEnumerator BloodEffect(RaycastHit hit)
    {
        GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

        yield return new WaitForSeconds(1.5f);

        Destroy(blood);
    }
}

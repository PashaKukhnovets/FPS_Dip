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
    [SerializeField] private PuzzleBehaviour puzzleBehaviour;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject shotgun;

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
                PlayerRayCast();
            }
        }
        else if (pistol.activeSelf){
            if (Input.GetButtonDown("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
               pistol.GetComponent<PistolBehaviour>().GetCurrentBulletCount() > 0)
            {
                pistolMuzzleEffect.Play();
                nextShoot = Time.time + 1.0f / rate;
                pistol.GetComponent<PistolBehaviour>().AddCurrentBulletCount(-1);
                PlayerRayCast();
            }
        }
        else if (shotgun.activeSelf)
        {
            if (Input.GetButtonDown("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
               shotgun.GetComponent<ShotgunBehaviour>().GetCurrentBulletCount() > 0)
            {
                shotgunMuzzleEffect.Play();
                nextShoot = Time.time + 1.0f / rate;
                shotgun.GetComponent<ShotgunBehaviour>().AddCurrentBulletCount(-1);
                PlayerRayCast();
            }
        }

    }

    private void PlayerRayCast() {

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
                    hitObject.GetComponent<TerroristController>().HitByPlayer();
                    StartCoroutine(BloodEffect(hit));
                }
                if (hitObject.gameObject.GetComponent<SecondTerroristController>())
                {
                    hitObject.GetComponent<SecondTerroristController>().HitByPlayer();
                    StartCoroutine(BloodEffect(hit));
                }
                if (hitObject.gameObject.GetComponent<ThirdTerroristController>())
                {
                    hitObject.GetComponent<ThirdTerroristController>().HitByPlayer();
                    StartCoroutine(BloodEffect(hit));
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private AKAnimationController weaponAnim;
    [SerializeField] private PuzzleBehaviour puzzleBehaviour;

    private float damage;
    private float rate = 7.0f;
    private float nextShoot = 0.0f;
    private GameObject weapon;

    void OnGUI()
    {
        int size = 20;
        float posX = playerCamera.pixelWidth / 2 - size / 4;
        float posY = playerCamera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+");
    }

    private void Start()
    {
        FindPlayerWeapon();
    }

    void Update()
    {
        Shoot();
        FindPlayerWeapon();
    }

    private void Shoot()
    {
        //if(проверка на один тип оружия){
        //    if (Input.GetButton("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
        //       weapon.GetComponent<AKBehaviour>().GetCurrentBulletCount() > 0)
        //    {
        //        .......................
        //    }
        //    }
        //else if(проверка на другой тип оружия){
        //    if (Input.GetButton("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
        //       weapon.GetComponent<AKBehaviour>().GetCurrentBulletCount() > 0)
        //    {
        //        ...............................
        //    }
        //    }
        //и тд

        if (Input.GetButton("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse() &&
            weapon.GetComponent<AKBehaviour>().GetCurrentBulletCount() > 0)
        {
            nextShoot = Time.time + 1.0f / rate;

            muzzleEffect.Play();

            mouseLook.ChangeOffsetRecoil(Random.Range(0.0f, 1.7f), Random.Range(-1.9f, 1.9f));

            Vector3 point = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
            Ray ray = playerCamera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.gameObject.CompareTag("Terrorist"))
                {
                    if (hitObject.gameObject.GetComponent<TerroristController>()) {
                        hitObject.GetComponent<TerroristController>().HitByPlayer();
                        StartCoroutine(BloodEffect(hit));
                    }
                    if (hitObject.gameObject.GetComponent<SecondTerroristController>()) {
                        hitObject.GetComponent<SecondTerroristController>().HitByPlayer();
                        StartCoroutine(BloodEffect(hit));
                    }
                }
            }
        }
    }

    private void FindPlayerWeapon() {
        weapon = GameObject.FindGameObjectWithTag("PlayerWeapon");
    }

    private IEnumerator BloodEffect(RaycastHit hit)
    {
        GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

        yield return new WaitForSeconds(1.5f);

        Destroy(blood);
    }
}

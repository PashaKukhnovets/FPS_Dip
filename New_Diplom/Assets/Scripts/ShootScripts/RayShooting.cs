using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private MouseLook mouseLook;
    [SerializeField] private WeaponAnimationController weaponAnim;

    private float damage;
    private float rate = 7.0f;
    private float nextShoot = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

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
        if (Input.GetButton("Fire1") && Time.time > nextShoot && !weaponAnim.IsBlockedMouse())
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

                if (hitObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("Человек");
                    StartCoroutine(BloodEffect(hit));
                }
                else if (hitObject.GetComponent<BoxCollider>())
                {
                    StartCoroutine(HoleIndicator(hit.point));
                    StartCoroutine(BloodEffect(hit));
                }

            }
        }
    }

    private IEnumerator HoleIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere); //Instantiate
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }

    private IEnumerator BloodEffect(RaycastHit hit)
    {
        GameObject blood = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));

        yield return new WaitForSeconds(1.5f);

        Destroy(blood);
    }
}

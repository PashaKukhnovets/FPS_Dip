using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooting : MonoBehaviour
{
    [SerializeField] private ParticleSystem muzzleEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private Camera playerCamera;
    private float damage;
    private float rate;
    private float nextShoot = 0.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnGUI()
    {
        int size = 40;
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
        if (Input.GetButton("Fire1") && Time.time > nextShoot)
        {
            nextShoot = Time.time + 1.0f / rate;

            audioSource.PlayOneShot(audioClip);
            muzzleEffect.Play();

            Vector3 point = new Vector3(playerCamera.pixelWidth / 2, playerCamera.pixelHeight / 2, 0);
            Ray ray = playerCamera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;

                if (hitObject.GetComponent<Rigidbody>())
                {
                    Debug.Log("Человек");
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
                else if (hitObject.GetComponent<BoxCollider>())
                {
                    StartCoroutine(HoleIndicator(hit.point));
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
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
}

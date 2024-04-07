using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ChangeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform rightTarget;
    [SerializeField] private Transform leftTarget;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject shotgun;
    [SerializeField] private GameObject grenade;
    [SerializeField] private PlayerController player;

    private int RandAnimReload = 0;
    private AKAnimationController animController;
    private bool wasAK = false;

    public bool isPistol = true;
    public Animation WeaponAnim;
    public AnimationClip[] AnimFire;
    public AnimationClip AnimGet;
    public AnimationClip AnimRemove;
    public AnimationClip[] AnimReload;

    void Start()
    {
        animController = gameObject.GetComponent<AKAnimationController>();

        if (isPistol)
        {
            SetToPistol();
            leftTarget.localPosition = new Vector3(-0.091f, -0.438f, 0.66f);
            leftTarget.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            rightTarget.localPosition = new Vector3(0.053f, -0.441f, -0.026f);
            rightTarget.localRotation = Quaternion.Euler(12.535f, -262.3f, -86.8f);
        }
        else
            SetToAk();
    }

    void Update()
    {
        WeaponGet();
    }

    private void WeaponGet()
    {
        if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) &&
            !WeaponAnim.IsPlaying(AnimRemove.name) && (Input.GetAxis("Mouse ScrollWheel") > 0 || Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            animController.SetBlockMouse(true);
            WeaponAnim.Stop();
            WeaponAnim.Play(AnimGet.name);

            if (!isPistol)
            {
                isPistol = true;
                SetToPistol();
            }
            else if (isPistol && player.GetUseAK())
            {
                isPistol = false;
                SetToAk();
            }
            else if (isPistol && player.GetUseShotgun()) {
                isPistol = false;
                SetToShotgun();
            }

            StartCoroutine(UnblockMouse());
        }

        if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) &&
            !WeaponAnim.IsPlaying(AnimRemove.name) && Input.GetKeyDown(KeyCode.Z) && player.GetUseGrenade())
        {
            isPistol = false;
            SetToGrenade();
            WeaponAnim.Stop();
            WeaponAnim.Play(AnimGet.name);
        }
    }

    public void SetToPistol() {
        if (wasAK) {
            this.gameObject.GetComponent<RigBuilder>().enabled = true;
            leftTarget.localPosition = new Vector3(-0.091f, -0.438f, 0.66f);
            leftTarget.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            rightTarget.localPosition = new Vector3(0.0712f, 0.1031f, -0.031f);
            rightTarget.localRotation = Quaternion.Euler(3.538f, -181.89f, -90.29f);
            isPistol = true;
            ak.SetActive(false);
            shotgun.SetActive(false);
            pistol.SetActive(true);
            grenade.SetActive(false);
        } 
        else {
            this.gameObject.GetComponent<RigBuilder>().enabled = true;
            leftTarget.localPosition = new Vector3(-0.091f, -0.438f, 0.66f);
            leftTarget.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            rightTarget.localPosition = new Vector3(0.078f, -0.45f, -0.036f);
            rightTarget.localRotation = Quaternion.Euler(3.993f, -268.5f, -86.59f);
            isPistol = true;
            ak.SetActive(false);
            shotgun.SetActive(false);
            pistol.SetActive(true);
            grenade.SetActive(false);
        }
    }

    private void SetToAk() {
        this.gameObject.GetComponent<RigBuilder>().enabled = false;
        ak.SetActive(true);
        pistol.SetActive(false);
        shotgun.SetActive(false);
        grenade.SetActive(false);
        if (!wasAK) {
            wasAK = true;
        }
    }

    private void SetToShotgun()
    {
        if (!wasAK)
        {
            this.gameObject.GetComponent<RigBuilder>().enabled = true;
            leftTarget.localPosition = new Vector3(-0.232f, -0.285f, -0.111f);
            leftTarget.localRotation = Quaternion.Euler(-157.8f, 299.1f, -273.3f);
            rightTarget.localPosition = new Vector3(-0.55f, -3.36f, 0.17f);
            rightTarget.localRotation = Quaternion.Euler(1.325f, -257.5f, -91.94f);
            ak.SetActive(false);
            pistol.SetActive(false);
            shotgun.SetActive(true);
            grenade.SetActive(false);
        }
        else {
            this.gameObject.GetComponent<RigBuilder>().enabled = true;
            leftTarget.localPosition = new Vector3(0.054f, -0.111f, 0.042f);
            leftTarget.localRotation = Quaternion.Euler(-20.41f, 43.41f, -105.3f);
            rightTarget.localPosition = new Vector3(-0.55f, -3.36f, 0.17f);
            rightTarget.localRotation = Quaternion.Euler(1.325f, -257.5f, -91.94f);
            ak.SetActive(false);
            pistol.SetActive(false);
            shotgun.SetActive(true);
            grenade.SetActive(false);
        }
    }

    private void SetToGrenade()
    {
        if (!wasAK)
        {
            this.gameObject.GetComponent<RigBuilder>().enabled = true;
            grenade.transform.localPosition = new Vector3(-0.185f, 0.282f, -0.1f);
            leftTarget.localPosition = new Vector3(0.06f, -6.41f, -0.49f);
            leftTarget.localRotation = Quaternion.Euler(-22.2f, 119.1f, -93.3f);
            rightTarget.localPosition = new Vector3(0.005f, -0.374f, -0.073f);
            rightTarget.localRotation = Quaternion.Euler(1.325f, -257.5f, -91.94f);
            ak.SetActive(false);
            pistol.SetActive(false);
            shotgun.SetActive(false);
            grenade.SetActive(true);
        }
        else
        {
            this.gameObject.GetComponent<RigBuilder>().enabled = true;
            grenade.transform.localPosition = new Vector3(-0.187f, 0.212f, -0.12f);
            leftTarget.localPosition = new Vector3(-1.25f, -15.43f, -0.23f);
            leftTarget.localRotation = Quaternion.Euler(-20.41f, 43.41f, -105.3f);
            rightTarget.localPosition = new Vector3(0.086f, 0.713f, 0.092f);
            rightTarget.localRotation = Quaternion.Euler(10.843f, -184.7f, -90.99f);
            ak.SetActive(false);
            pistol.SetActive(false);
            shotgun.SetActive(false);
            grenade.SetActive(true);
        }
    }

    private IEnumerator UnblockMouse()
    {
        yield return new WaitForSeconds(0.5f);
        animController.SetBlockMouse(false);
    }
}

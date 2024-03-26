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
    [SerializeField] private PlayerController player;

    private int RandAnimReload = 0;
    private AKAnimationController animController;

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
            else if (isPistol && player.GetUseAK()) {
                isPistol = false;
                SetToAk();
            }

            StartCoroutine(UnblockMouse());
        }
    }

    public void SetToPistol() {
        this.gameObject.GetComponent<RigBuilder>().enabled = true;
        leftTarget.localPosition = new Vector3(-0.091f, -0.438f, 0.66f);
        leftTarget.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        rightTarget.localPosition = new Vector3(0.0712f, 0.1031f, -0.031f);
        rightTarget.localRotation = Quaternion.Euler(3.538f, -181.89f, -90.29f);
        ak.SetActive(false);
        pistol.SetActive(true);
    }

    private void SetToAk() {
        this.gameObject.GetComponent<RigBuilder>().enabled = false;
        ak.SetActive(true);
        pistol.SetActive(false);
    }

    private IEnumerator UnblockMouse()
    {
        yield return new WaitForSeconds(0.5f);
        animController.SetBlockMouse(false);
    }
}

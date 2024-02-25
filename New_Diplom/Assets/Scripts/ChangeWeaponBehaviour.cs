using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponBehaviour : MonoBehaviour
{
    [SerializeField] private Transform rightTarget;
    [SerializeField] private Transform leftTarget;
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject ak;

    private int RandAnimReload = 0;
    private AKAnimationController animController;
    private bool isPistol = false;

    public Animation WeaponAnim;
    public AnimationClip[] AnimFire;
    public AnimationClip AnimGet;
    public AnimationClip AnimRemove;
    public AnimationClip[] AnimReload;

    void Start()
    {
        animController = gameObject.GetComponent<AKAnimationController>();
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
                ak.SetActive(false);
                pistol.SetActive(true);
                leftTarget.localPosition = new Vector3(-0.091f, -0.438f, 0.66f);
                leftTarget.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                rightTarget.localPosition = new Vector3(0.065f, -0.402f, -0.038f);
                rightTarget.localRotation = Quaternion.Euler(12.535f, -262.3f, -86.8f);

            }
            else if (isPistol) {
                isPistol = false;
                ak.SetActive(true);
                pistol.SetActive(false);
                leftTarget.localPosition = new Vector3(-0.45f, -0.247f, -0.014f);
                leftTarget.localRotation = Quaternion.Euler(-146.9f, 351.09f, -280.1f);
                rightTarget.localPosition = new Vector3(0.094f, -0.49f, -0.077f);
                rightTarget.localRotation = Quaternion.Euler(-2.969f, -265.2f, -90.02f);
            }

            StartCoroutine(UnblockMouse());
        }
    }

    private IEnumerator UnblockMouse()
    {
        yield return new WaitForSeconds(2.0f);
        animController.SetBlockMouse(false);
    }
}

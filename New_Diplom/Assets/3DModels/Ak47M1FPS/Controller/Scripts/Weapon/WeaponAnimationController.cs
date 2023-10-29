using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class WeaponAnimationController : MonoBehaviour
{
    public Animation WeaponAnim;
    public AnimationClip[] AnimIdle;
    public AnimationClip[] AnimWalk;
    public AnimationClip[] AnimFire;
    public AnimationClip AnimIdleWalkToRun;
    public AnimationClip[] AnimRun;
    public AnimationClip AnimRunToAnimIdleWalk;
    public AnimationClip AnimGet;
    public AnimationClip AnimRemove;    
    public AnimationClip AnimAimUp;
    public AnimationClip[] AnimAimFire;
    public AnimationClip AnimAimDown;
    public AnimationClip[] AnimReload;

    public AudioClip audioFire;
    private AudioSource audioSource;

    private bool isAiming = false;
    private int RandAnimReload = 0;
    private float rate = 7.0f;
    private float nextShoot = 0.0f;

    private bool blockMouse = false;


    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update() {
        Fire();
        Reload();
        AimingUp();
        AimingDown();
        Walking();
    }

    private void Fire() {
        if (!isAiming && !blockMouse)
        {
            if (Input.GetButton("Fire1") && Time.time > nextShoot)
            {
                nextShoot = Time.time + 1.0f / rate;

                FirePlay();
            }
        }
    }

    private void Reload() {
        if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimRemove.name) && Input.GetKeyDown(KeyCode.R))
        {
            blockMouse = true;
            RandAnimReload = Random.Range(0, AnimReload.Length);
            ReloadPlay();
            StartCoroutine(UnblockMouse());
        }
    }

    private void AimingUp() {
        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimAimUp.name) && Input.GetMouseButtonDown(1))
        {
            AimingUp();
        }
    }

    private void AimingDown() {
        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimAimDown.name) && Input.GetMouseButtonUp(1))
        {
            AimingDown();
        }

        if (WeaponAnim.IsPlaying(AnimRemove.name))
            return;
    }

    private void Walking() {
        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimGet.name) && isAiming == false && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1F)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                WeaponAnim.CrossFade(AnimRun[0].name);
                blockMouse = true;
            }
            else
            {
                WeaponAnim.CrossFade(AnimWalk[0].name);
                blockMouse = false;
            }
        }
        else if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimGet.name) && isAiming == false)
        {
            WeaponAnim.CrossFade(AnimIdle[0].name);
        }

    }

    private void FirePlay()
    {
        WeaponAnim.Stop();

        if (!isAiming )
        {
            Debug.Log("AAA");
            WeaponAnim.Play(AnimFire[0].name);
        }
        else
        {
            WeaponAnim.Play(AnimAimFire[0].name);
        }

        audioSource.clip = audioFire;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void ReloadPlay()
    {
        WeaponAnim.Stop();
        WeaponAnim.Play(AnimReload[RandAnimReload].name);
    }

    private void AimingUpPlay()
    {
        WeaponAnim.Stop();
        WeaponAnim.Play(AnimAimUp.name);
        isAiming = true;
    }

    private void AimingDownPlay()
    {
        WeaponAnim.Stop();
        WeaponAnim.Play(AnimAimDown.name);
        isAiming = false;
    }

    public bool IsBlockedMouse() {
        return this.blockMouse;
    }

    private IEnumerator UnblockMouse() {
        yield return new WaitForSeconds(2.0f);
        blockMouse = false;
    } 

}

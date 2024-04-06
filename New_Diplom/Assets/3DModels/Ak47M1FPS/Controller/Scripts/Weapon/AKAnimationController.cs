using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]

public class AKAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private GameObject ak;
    [SerializeField] private GameObject shotgun;

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
    private bool isSprint = false;

    public event UnityAction RefillAK;

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
        if (!blockMouse)
        {
            if (pistol.activeSelf)
            {
                if (Input.GetButtonDown("Fire1") && Time.time > nextShoot && pistol.GetComponent<PistolBehaviour>().GetCurrentBulletCount() > 0)
                {
                    nextShoot = Time.time + 1.0f / rate;

                    FirePlay();
                }
            }
            else if (ak.activeSelf) {
                if (Input.GetButton("Fire1") && Time.time > nextShoot && ak.GetComponent<AKBehaviour>().GetCurrentBulletCount() > 0)
                {
                    nextShoot = Time.time + 1.0f / rate;

                    FirePlay();
                }
            }
            else if (shotgun.activeSelf)
            {
                if (Input.GetButtonDown("Fire1") && Time.time > nextShoot && shotgun.GetComponent<ShotgunBehaviour>().GetCurrentBulletCount() > 0)
                {
                    nextShoot = Time.time + 1.0f / 1.5f;

                    FirePlay();
                }
            }
        }
    }

    private void Reload() {
        if (pistol.activeSelf)
        {
            if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) &&
                !WeaponAnim.IsPlaying(AnimRemove.name) && Input.GetKeyDown(KeyCode.R) && pistol.GetComponent<PistolBehaviour>().GetAmountOfBullets() > 0 &&
                pistol.GetComponent<PistolBehaviour>().GetCurrentBulletCount() != 7)
            {
                blockMouse = true;
                RandAnimReload = Random.Range(0, AnimReload.Length);
                ReloadPlay();
                StartCoroutine(UnblockMouse());
            }
        }
        else if (ak.activeSelf) {
            if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) &&
                    !WeaponAnim.IsPlaying(AnimRemove.name) && Input.GetKeyDown(KeyCode.R) && ak.GetComponent<AKBehaviour>().GetAmountOfBullets() > 0 &&
                    ak.GetComponent<AKBehaviour>().GetCurrentBulletCount() != 30)
            {
                blockMouse = true;
                RandAnimReload = Random.Range(0, AnimReload.Length);
                ReloadPlay();
                StartCoroutine(UnblockMouse());
            }
        }
        else if (shotgun.activeSelf)
        {
            if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) &&
                    !WeaponAnim.IsPlaying(AnimRemove.name) && Input.GetKeyDown(KeyCode.R) && shotgun.GetComponent<ShotgunBehaviour>().GetAmountOfBullets() > 0 &&
                    shotgun.GetComponent<ShotgunBehaviour>().GetCurrentBulletCount() != 5)
            {
                blockMouse = true;
                RandAnimReload = Random.Range(0, AnimReload.Length);
                ReloadPlay();
                StartCoroutine(UnblockMouse());
            }
        }
    }

    private void AimingUp() {
        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimAimUp.name) && 
            Input.GetMouseButtonDown(1) && !isSprint)
        {
            AimingUpPlay();
            
        }
    }

    private void AimingDown() {
        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimAimDown.name) && Input.GetMouseButtonUp(1) && !isSprint)
        {
            AimingDownPlay();
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
                isSprint = true;
            }
            else
            {
                WeaponAnim.CrossFade(AnimWalk[0].name);
                blockMouse = false;
                isSprint = false;
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
            WeaponAnim.Play(AnimFire[0].name);
        }
        //else if (Input.GetMouseButtonDown(0) && Input.GetMouseButtonDown(1))
        //{
        //    WeaponAnim.Play(AnimAimFire[0].name);
        //}

        audioSource.clip = audioFire;
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void ReloadPlay()
    {
        RefillAK?.Invoke();
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

    public void SetBlockMouse(bool value) {
        this.blockMouse = value;
    }

    public bool IsBlockedMouse() {
        return this.blockMouse;
    }

    private IEnumerator UnblockMouse() {
        yield return new WaitForSeconds(2.0f);
        blockMouse = false;
    } 

}

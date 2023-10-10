using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class sWeaponFPS : MonoBehaviour
{
    [Header("ANIMATION CONTROL")]
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

    [Header("AUDIO CONTROL")]
    public AudioClip a_AudioFire;
    private AudioSource a_AudioSource;

    public static sWeaponFPS instance;
    private bool IsAiming = false;
    private int RandAnimReload = 0;

    void OnEnable()
    {
        instance = this;
    }

    void Start ()
    {
        a_AudioSource = GetComponent<AudioSource>();
    }
	
	void Update ()
    {
        //
        // Reload
        //

        if (!WeaponAnim.IsPlaying(AnimFire[0].name) && !WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimRemove.name) && Input.GetKeyDown(KeyCode.R))
        {
            RandAnimReload = Random.Range(0, AnimReload.Length);
            reload();
        }

        //
        // AimUp
        //

        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimAimUp.name) && Input.GetMouseButtonDown(1))
        {
            AimingUp();
        }  

        //
        // Fire
        //

        if (IsAiming == false)
        {
            if (Input.GetMouseButton(0))
            {
                fire();
            }
        }

        //
        // AimDown
        //

        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimAimDown.name) && Input.GetMouseButtonUp(1))
        {
            AimingDown();
        }

        if (WeaponAnim.IsPlaying(AnimRemove.name))
            return;

        //
        // Idle \ Walk \ Run
        //

        if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimGet.name) && IsAiming == false && Mathf.Abs(Input.GetAxis("Vertical")) > 0.1F)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                WeaponAnim.CrossFade(AnimRun[0].name);
            }
            else
            {
                WeaponAnim.CrossFade(AnimWalk[0].name);
            }
        }
        else if (!WeaponAnim.IsPlaying(AnimReload[RandAnimReload].name) && !WeaponAnim.IsPlaying(AnimGet.name) && IsAiming == false)
        {
            WeaponAnim.CrossFade(AnimIdle[0].name);
        }
    }


    void fire()
    {
        WeaponAnim.Stop();

        if (!IsAiming)
            WeaponAnim.Play(AnimFire[0].name);
        else
            WeaponAnim.Play(AnimAimFire[0].name);

        a_AudioSource.clip = a_AudioFire;
        a_AudioSource.PlayOneShot(a_AudioSource.clip);
    }

    void reload()
    {
        WeaponAnim.Stop();
        WeaponAnim.Play(AnimReload[RandAnimReload].name);
    }

    void AimingUp()
    {
        WeaponAnim.Stop();
        WeaponAnim.Play(AnimAimUp.name);
        IsAiming = true;
    }

    void AimingDown()
    {
        WeaponAnim.Stop();
        WeaponAnim.Play(AnimAimDown.name);
        IsAiming = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTerroristAnimController : MonoBehaviour
{
    [SerializeField] private AudioSource thirdTerroristWalk;

    private ThirdTerroristController terrorist;
    private Pursue terroristPursue;
    private Animator animator;
    private bool isWalkSound = false;

    void Start()
    {
        terrorist = this.GetComponent<ThirdTerroristController>();
        animator = this.GetComponent<Animator>();
        terroristPursue = this.GetComponent<Pursue>();
        terroristPursue.ThirdTerroristAgrWalking += ThirdTerroristAgrWalking;
        terroristPursue.ThirdTerroristAttacking += ThirdTerroristAttacking;
        terroristPursue.ThirdTerroristAttackingFalse += ThirdTerroristAttackingFalse;
        terrorist.ThirdTerroristStunning += ThirdTerroristStunning;
        terrorist.ThirdTerroristStunningFalse += ThirdTerroristStunningFalse;
        terrorist.ThirdTerroristDeath += ThirdTerroristDeath;
    }

    public void ThirdTerroristAgrWalking()
    {
        animator.SetBool("isAgrWalking", true);
        if (!isWalkSound) {
            isWalkSound = true;
            thirdTerroristWalk.Play();
        }
    }

    public void ThirdTerroristAttacking()
    {
        animator.SetBool("isAgrWalking", false);
        animator.SetBool("isAttacking", true);
        isWalkSound = false;
    }

    public void ThirdTerroristAttackingFalse()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isAgrWalking", true);
        if (!isWalkSound)
        {
            isWalkSound = true;
            thirdTerroristWalk.Play();
        }
    }

    public void ThirdTerroristStunning() {
        animator.SetBool("isStunning", true);
        thirdTerroristWalk.Stop();
    }

    public void ThirdTerroristStunningFalse() {
        animator.SetBool("isStunning", false);
        animator.SetBool("isAgrWalking", true);
        if (!isWalkSound)
        {
            isWalkSound = true;
            thirdTerroristWalk.Play();
        }
    }

    public void ThirdTerroristDeath()
    {
        thirdTerroristWalk.Stop();
        animator.SetBool("isDeath", true);
    }

    public void SetWalkSoundVariable(bool value) {
        isWalkSound = value;
    }
}

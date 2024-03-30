using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTerroristAnimController : MonoBehaviour
{
    private ThirdTerroristController terrorist;
    private Pursue terroristPursue;
    private Animator animator;

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
    }

    public void ThirdTerroristAttacking()
    {
        animator.SetBool("isAgrWalking", false);
        animator.SetBool("isAttacking", true);
    }

    public void ThirdTerroristAttackingFalse()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isAgrWalking", true);
    }

    public void ThirdTerroristStunning() {
        animator.SetBool("isStunning", true);
    }

    public void ThirdTerroristStunningFalse() {
        animator.SetBool("isStunning", false);
        animator.SetBool("isAgrWalking", true);
    }

    public void ThirdTerroristDeath()
    {
        animator.SetBool("isDeath", true);
    }
}

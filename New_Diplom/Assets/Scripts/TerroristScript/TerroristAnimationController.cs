using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristAnimationController : MonoBehaviour
{
    private TerroristController terrorist;
    private Pursue terroristPursue;
    private Animator animator;

    void Start()
    {
        terrorist = this.GetComponent<TerroristController>();
        animator = this.GetComponent<Animator>();
        terroristPursue = this.GetComponent<Pursue>();
        terroristPursue.TerroristRunning += TerroristRunning;
        terroristPursue.TerroristStandFire += TerroristStandFire;
        terroristPursue.TerroristStandFireFalse += TerroristStandFireFalse;
        terrorist.TerroristRunFire += TerroristRunFire;
        terrorist.TerroristRunFireFalse += TerroristRunFireFalse;
        terrorist.TerroristDeath += TerroristDeath;
    }

    public void TerroristRunning()
    {
        animator.SetBool("isRunning", true);
    }

    public void TerroristRunFire()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isRunFire", true);
    }

    public void TerroristRunFireFalse()
    {
        animator.SetBool("isRunFire", false);
        animator.SetBool("isRunning", true);
    }

    public void TerroristStandFire() {
        animator.SetBool("isStandFire", true);
        animator.SetBool("isRunFire", false);
    }

    public void TerroristStandFireFalse()
    {
        animator.SetBool("isStandFire", false);
        animator.SetBool("isRunFire", true);
    }

    public void TerroristDeath()
    {
        animator.SetBool("isDeath", true);
    }
}

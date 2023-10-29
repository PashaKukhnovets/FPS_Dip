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
        terrorist.TerroristAttack += TerroristAttack;
        terrorist.TerroristAttackFalse += TerroristAttackFalse;
        terrorist.TerroristDeath += TerroristDeath;
    }

    public void TerroristRunning()
    {
        animator.SetBool("isRunning", true);
    }

    public void TerroristAttack()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isAttacking", true);
    }

    public void TerroristAttackFalse()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isRunning", true);
    }

    public void TerroristDeath()
    {
        animator.SetBool("isDeath", true);
    }
}

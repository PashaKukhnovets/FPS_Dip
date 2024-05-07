using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerroristAnimationController : MonoBehaviour
{
    [SerializeField] private AudioSource terroristWalk;
    [SerializeField] private AudioSource terroristRun;

    private TerroristController terrorist;
    private Pursue terroristPursue;
    private Animator animator;
    private bool isRunSound = false;
    private bool isWalkSound = false;

    public bool isWalk = false;

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

        if (isWalk)
        {
            animator.SetBool("isWalkIdle", true);
            if (!isWalkSound)
            {
                terroristWalk.Play();
                isWalkSound = true;
            }
        }
    }

    public void TerroristRunning()
    {
        animator.SetBool("isRunning", true);
        terroristWalk.Stop();
        if (!isRunSound)
        {
            isRunSound = true;
            isWalkSound = false;
            terroristRun.Play();
        }
    }

    public void TerroristRunFire()
    {
        animator.SetBool("isRunning", false);
        animator.SetBool("isRunFire", true);
        terroristRun.Stop();
        if (!isWalkSound)
        {
            isWalkSound = true;
            isRunSound = false;
            terroristWalk.Play();
        }
    }

    public void TerroristRunFireFalse()
    {
        animator.SetBool("isRunFire", false);
        animator.SetBool("isRunning", true);
        terroristWalk.Stop();
        if (!isRunSound)
        {
            isRunSound = true;
            isWalkSound = false;
            terroristRun.Play();
        }
    }

    public void TerroristStandFire() {
        animator.SetBool("isStandFire", true);
        animator.SetBool("isRunFire", false);
        terroristWalk.Stop();
        terroristRun.Stop();
        isRunSound = false;
        isWalkSound = false;
    }

    public void TerroristStandFireFalse()
    {
        animator.SetBool("isStandFire", false);
        animator.SetBool("isRunFire", true);
        terroristRun.Stop();
        if (!isWalkSound)
        {
            isRunSound = false;
            isWalkSound = true;
            terroristWalk.Play();
        }
    }

    public void TerroristDeath()
    {
        terroristWalk.Stop();
        terroristRun.Stop();
        animator.SetBool("isDeath", true);
    }
}

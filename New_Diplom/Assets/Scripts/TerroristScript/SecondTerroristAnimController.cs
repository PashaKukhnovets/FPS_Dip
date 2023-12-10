using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTerroristAnimController : MonoBehaviour
{
    private SecondTerroristController terrorist;
    private Animator animator;

    void Start()
    {
        terrorist = this.GetComponent<SecondTerroristController>();
        animator = this.GetComponent<Animator>();
        terrorist.TerroristSitFire += TerroristSitFire;
        terrorist.TerroristSitFireFalse += TerroristSitFireFalse;
        terrorist.TerroristDeath += TerroristDeath;
    }

    public void TerroristSitFire()
    {
        animator.SetBool("IsRifleSit", true);
    }

    public void TerroristSitFireFalse()
    {
        animator.SetBool("IsRifleSit", false);
    }

    public void TerroristDeath()
    {
        animator.SetBool("IsDeathSit", true);
    }
}

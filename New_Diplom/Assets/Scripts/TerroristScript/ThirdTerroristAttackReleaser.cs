using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTerroristAttackReleaser : StateMachineBehaviour
{
    [SerializeField] private ParticleSystem playerBlood;
    [SerializeField] private AudioClip thirdTerroristAttack;

    private GameObject player;
    private float beginPoint = 0.0f;
    private bool check = true;
    private bool isNextPlaying;
    private float damage;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (check)
        {
            isNextPlaying = (Time.time - beginPoint) > stateInfo.length / 2.0f;
            if (isNextPlaying)
            {
                ThirdTerroristAttack();
                beginPoint = Time.time;
                check = false;
            }
        }
        else
        {
            isNextPlaying = (Time.time - beginPoint) > stateInfo.length;
            if (isNextPlaying)
            {
                ThirdTerroristAttack();
                beginPoint = Time.time;
            }
        }

    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        beginPoint = Time.time;
        check = true;
        damage = animator.GetComponent<ThirdTerroristController>().damage;
    }

    public void ThirdTerroristAttack()
    {
        PlayerParameters.AddPlayerDamage(damage);
        AudioSource.PlayClipAtPoint(thirdTerroristAttack, player.transform.position);
        Instantiate(playerBlood, new Vector3(player.transform.position.x + 0.3f, 1.1f, player.transform.position.z), Quaternion.identity);
    }
}

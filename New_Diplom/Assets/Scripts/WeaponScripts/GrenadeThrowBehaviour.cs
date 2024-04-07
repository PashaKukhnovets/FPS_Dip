using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion;

    private bool isDamageFirst = false;
    private bool isDamageSecond = false;
    private bool isDamageThird = false;

    void Start()
    {
        StartCoroutine(GrenadeThrowDelete());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<TerroristController>() && isDamageFirst) 
        {
            isDamageFirst = false;
            other.gameObject.GetComponent<TerroristController>().HitByPlayer(true);
        }

        if (other.gameObject.GetComponent<SecondTerroristController>() && isDamageSecond)
        {
            isDamageSecond = false;
            other.gameObject.GetComponent<SecondTerroristController>().HitByPlayer(true);
        }

        if (other.gameObject.GetComponent<ThirdTerroristController>() && isDamageThird)
        {
            isDamageThird = false;
            other.gameObject.GetComponent<ThirdTerroristController>().HitByPlayer(true);
        }
    }

    private IEnumerator GrenadeThrowDelete()
    {
        yield return new WaitForSeconds(5.0f);
        isDamageFirst = true;
        isDamageSecond = true;
        isDamageThird = true;
        yield return new WaitForSeconds(0.1f);
        Instantiate(explosion, transform.position, Quaternion.Euler(0.0f,0.0f,0.0f));
        Destroy(this.gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrowBehaviour : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GrenadeThrowDelete());
    }

    private IEnumerator GrenadeThrowDelete()
    {
        yield return new WaitForSeconds(5.0f);
        Destroy(this.gameObject);
    }
}

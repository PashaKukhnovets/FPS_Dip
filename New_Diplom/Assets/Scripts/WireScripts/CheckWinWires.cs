using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWinWires : MonoBehaviour
{
    [SerializeField] private GameObject upperLock;
    [SerializeField] private int countOfWires;

    private int endCountOfWires;
    private bool isEndPuzzles = false; 

    void Update()
    {
        CheckWin();
    }

    private void CheckWin()
    {
        if (countOfWires == endCountOfWires)
        {
            if (!isEndPuzzles)
            {
                isEndPuzzles = true;
                StartCoroutine(OffPuzzles());
            }
        }
    }

    public bool IsEndWires() {
        return isEndPuzzles;
    }

    private IEnumerator OffPuzzles()
    {
        yield return new WaitForSeconds(1.0f);

        upperLock.gameObject.transform.localPosition = new Vector3(0.0245f, 0.07f, 0.0f);
        upperLock.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        yield return new WaitForSeconds(2.0f);

        PlayerParameters.SetWindowOpen(false);
        this.gameObject.SetActive(false);
    }

    public void AddEndCountOfWires()
    {
        endCountOfWires++;
    }
}

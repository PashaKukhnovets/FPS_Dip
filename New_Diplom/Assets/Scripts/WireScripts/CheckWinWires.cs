using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWinWires : MonoBehaviour
{
    [SerializeField] private GameObject[] wires;
    [SerializeField] private GameObject[] wireConnections;
    [SerializeField] private GameObject upperLock;

    private bool isEndPuzzles = false; 

    void Update()
    {
        CheckWin();
    }

    private void CheckWin()
    {
        if (Mathf.Abs(wires[0].transform.position.x - wireConnections[0].transform.position.x) <= 0.1f &&
            Mathf.Abs(wires[0].transform.position.y - wireConnections[0].transform.position.y) <= 0.1f &&
            Mathf.Abs(wires[1].transform.position.x - wireConnections[1].transform.position.x) <= 0.1f &&
            Mathf.Abs(wires[1].transform.position.y - wireConnections[1].transform.position.y) <= 0.1f &&
            Mathf.Abs(wires[2].transform.position.x - wireConnections[2].transform.position.x) <= 0.1f &&
            Mathf.Abs(wires[2].transform.position.y - wireConnections[2].transform.position.y) <= 0.1f &&
            Mathf.Abs(wires[3].transform.position.x - wireConnections[3].transform.position.x) <= 0.1f &&
            Mathf.Abs(wires[3].transform.position.y - wireConnections[3].transform.position.y) <= 0.1f)
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
}

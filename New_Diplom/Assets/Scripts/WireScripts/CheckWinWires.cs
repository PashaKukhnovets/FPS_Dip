using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWinWires : MonoBehaviour
{
    [SerializeField] private GameObject[] wires;
    [SerializeField] private GameObject[] wireConnections;
    [SerializeField] private GameObject winLabel;

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
                winLabel.SetActive(true);
                StartCoroutine(OffPuzzles());
            }
        }
    }

    private IEnumerator OffPuzzles()
    {
        yield return new WaitForSeconds(3.0f);

        this.gameObject.SetActive(false);
    }
}

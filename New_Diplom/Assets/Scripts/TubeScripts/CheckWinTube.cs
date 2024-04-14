using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CheckWinTube : MonoBehaviour
{
    [SerializeField] private GameObject[] tube;
    [SerializeField] private GameObject[] tubeForm;
    [SerializeField] private GameObject upperLock;

    private bool isEndPuzzles = false;

    void Update()
    {
        CheckWin();
    }

    private void CheckWin() {
        if (Mathf.Abs(tube[0].transform.position.x - tubeForm[0].transform.position.x) <= 0.3f &&
            Mathf.Abs(tube[0].transform.position.y - tubeForm[0].transform.position.y) <= 0.3f &&
            Mathf.Abs(tube[1].transform.position.x - tubeForm[1].transform.position.x) <= 0.3f &&
            Mathf.Abs(tube[1].transform.position.y - tubeForm[1].transform.position.y) <= 0.3f &&
            Mathf.Abs(tube[2].transform.position.x - tubeForm[2].transform.position.x) <= 0.3f &&
            Mathf.Abs(tube[2].transform.position.y - tubeForm[2].transform.position.y) <= 0.3f &&
            Mathf.Abs(tube[3].transform.position.x - tubeForm[3].transform.position.x) <= 0.3f &&
            Mathf.Abs(tube[3].transform.position.y - tubeForm[3].transform.position.y) <= 0.3f &&
            Mathf.Abs(tube[4].transform.position.x - tubeForm[4].transform.position.x) <= 0.3f &&
            Mathf.Abs(tube[4].transform.position.y - tubeForm[4].transform.position.y) <= 0.3f &&
            Mathf.Abs(tube[5].transform.position.x - tubeForm[5].transform.position.x) <= 0.3f &&
            Mathf.Abs(tube[5].transform.position.y - tubeForm[5].transform.position.y) <= 0.3f) {
            if (!isEndPuzzles) {
                isEndPuzzles = true;
                StartCoroutine(OffPuzzles());
            }
        }
    }

    public bool IsEndTubes()
    {
        return isEndPuzzles;
    }

    private IEnumerator OffPuzzles() {
        yield return new WaitForSeconds(1.0f);

        upperLock.gameObject.transform.localPosition = new Vector3(0.0245f, 0.07f, 0.0f);
        upperLock.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);

        yield return new WaitForSeconds(2.0f);

        PlayerParameters.SetWindowOpen(false);
        this.gameObject.SetActive(false);
    }
}

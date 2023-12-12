using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject puzzle;

    private bool isPuzzleActive = false;
    private bool isWire = false;
    private bool isTube = false;
    private bool isEndPuzzle = false;
    private bool isInTrigger = false;

    private void Start()
    {
        CheckTypeOfPuzzle();
    }

    private void Update()
    {
        OpenDoor();
        CheckExitPuzzle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (!isEndPuzzle)
            {
                puzzle.SetActive(true);
                isPuzzleActive = true;
                PlayerParameters.SetWindowOpen(true);
                isInTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            isInTrigger = false;
        }
    }

    private void OpenDoor() {
        if (isTube)
        {
            if (isPuzzleActive && puzzle.GetComponent<CheckWinTube>().IsEndTubes())
            {
                //GetComponent<Animator>().SetTrigger("DoorATrigger");
                this.gameObject.transform.Find("door").transform.rotation = Quaternion.Euler(0.0f, 100.0f, 0.0f);
                isEndPuzzle = true;
                if (this.gameObject.CompareTag("WinDoor")) {
                    SceneManager.LoadScene(0);
                }
            }
        }
        if (isWire) {
            if (isPuzzleActive && puzzle.GetComponent<CheckWinWires>().IsEndWires())
            {
                //GetComponent<Animator>().SetTrigger("DoorATrigger");
                this.gameObject.transform.Find("door").transform.rotation = Quaternion.Euler(0.0f, 100.0f, 0.0f);
                isEndPuzzle = true;
                if (this.gameObject.CompareTag("WinDoor"))
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }

    private void CheckTypeOfPuzzle() {
        if (puzzle.GetComponent<CheckWinTube>())
        {
            isTube = true;
        }
        else if (puzzle.GetComponent<CheckWinWires>())
        {
            isWire = true;
        }
    }

    private void CheckExitPuzzle() {
        if (Input.GetKeyDown(KeyCode.Escape) && isInTrigger)
        {
            StartCoroutine(PuzzleCLoseVariable());
        }
    }

    private IEnumerator PuzzleCLoseVariable() {
        yield return new WaitForSeconds(0.5f);
        PlayerParameters.SetWindowOpen(false);
        puzzle.SetActive(false);
    }
}
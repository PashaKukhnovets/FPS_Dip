using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject puzzle;

    private bool isPuzzleActive = false;
    private bool isWire = false;
    private bool isTube = false;
    private bool isEndPuzzle = false;

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
        if (!isEndPuzzle)
        {
            puzzle.SetActive(true);
            isPuzzleActive = true;
        }
    }

    private void OpenDoor() {
        if (isTube)
        {
            if (isPuzzleActive && puzzle.GetComponent<CheckWinTube>().IsEndTubes())
            {
                GetComponent<Animator>().SetTrigger("DoorATrigger");
                isEndPuzzle = true;
            }
        }
        if (isWire) {
            if (isPuzzleActive && puzzle.GetComponent<CheckWinWires>().IsEndWires())
            {
                GetComponent<Animator>().SetTrigger("DoorATrigger");
                isEndPuzzle = true;
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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            puzzle.SetActive(false);
        }
    }
}
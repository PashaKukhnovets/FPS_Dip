using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject puzzle;
    [SerializeField] private List<GameObject> terrorists;

    private PlayerController player;
    private GameBehaviour gameManager;
    private bool isPuzzleActive = false;
    private bool isWire = false;
    private bool isTube = false;
    private bool isEndPuzzle = false;
    private bool isInTrigger = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameBehaviour>();
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
            if (!isEndPuzzle && !this.gameObject.CompareTag("WinDoor"))
            {
                puzzle.SetActive(true);
                isPuzzleActive = true;
                PlayerParameters.SetWindowOpen(true);
                isInTrigger = true;
                player.StopPlayerSounds();
            }
            else if (!isEndPuzzle && this.gameObject.CompareTag("WinDoor"))
            {
                if (gameManager.GetCountOfPapers() == 7)
                {
                    puzzle.SetActive(true);
                    isPuzzleActive = true;
                    PlayerParameters.SetWindowOpen(true);
                    isInTrigger = true;
                    player.StopPlayerSounds();
                }
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
            if (isPuzzleActive && puzzle.GetComponent<CheckWinTube>().IsEndTubes() && !isEndPuzzle)
            {
                if (this.gameObject.CompareTag("OfficeDoor"))
                {
                    this.gameObject.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
                }
                else
                {
                    this.gameObject.transform.localRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);

                    if (this.gameObject.CompareTag("baddoor"))
                    {
                        this.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 310.0f, 0.0f);
                    }
                }

                foreach (var i in terrorists)
                {
                    i.SetActive(true);
                }

                isEndPuzzle = true;

                if (this.gameObject.CompareTag("WinDoor"))
                {
                    StartCoroutine(NextLevelPlay());
                }
            }
        }
        if (isWire) {
            if (isPuzzleActive && puzzle.GetComponent<CheckWinWires>().IsEndWires() && !isEndPuzzle)
            {
                if (this.gameObject.CompareTag("OfficeDoor"))
                {
                    this.gameObject.transform.localPosition = new Vector3(0.0f, 30.0f, 0.0f);
                }
                else
                {
                    this.gameObject.transform.localRotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);

                    if (this.gameObject.CompareTag("baddoor"))
                    {
                        this.gameObject.transform.localRotation = Quaternion.Euler(0.0f, 310.0f, 0.0f);
                    }
                }

                foreach (var i in terrorists)
                {
                    i.SetActive(true);
                }

                isEndPuzzle = true;

                if (this.gameObject.CompareTag("WinDoor"))
                {
                    StartCoroutine(NextLevelPlay());
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

    private IEnumerator NextLevelPlay() {
        yield return new WaitForSeconds(4.0f);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(3);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3) {
            SceneManager.LoadScene(4);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(0);
        }
    }
}
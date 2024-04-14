using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMoving : MonoBehaviour
{
    [SerializeField] private Camera puzzleCamera;
    [SerializeField] private List<GameObject> forms;
    [SerializeField] private CheckWinTube checkWinTube;

    private bool isMoving = false;
    private bool isEndMoving = false;
    private Vector3 mousePos;

    void Update()
    {
        PuzzleMoving();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && !isEndMoving)
        {
            isMoving = true;
            mousePos = Input.mousePosition;
        }
    }

    private void OnMouseUp()
    {
        if (!isEndMoving)
        {
            isMoving = false;

            if (forms.Count == 1)
            {
                CheckPuzzleForm(forms[0]);
            }
            else if (forms.Count == 2)
            {
                CheckPuzzleForm(forms[0]);
                CheckPuzzleForm(forms[1]);
            }
            else if (forms.Count == 3)
            {
                CheckPuzzleForm(forms[0]);
                CheckPuzzleForm(forms[1]);
                CheckPuzzleForm(forms[2]);
            }
        }
    }

    private void PuzzleMoving()
    {
        if (isMoving)
        {
            mousePos = Input.mousePosition;
            mousePos = puzzleCamera.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, -0.62f);
        }
    }

    private void CheckPuzzleForm(GameObject checkForm)
    {
        if (Mathf.Abs(this.transform.localPosition.x - checkForm.transform.localPosition.x) <= 0.15f &&
                        Mathf.Abs(this.transform.localPosition.y - checkForm.transform.localPosition.y) <= 0.15f)
        {
            this.transform.position = new Vector2(checkForm.transform.position.x, checkForm.transform.position.y);
            isEndMoving = true;
            checkWinTube.AddCountOffTubeEndPosition();
        }
    }

}
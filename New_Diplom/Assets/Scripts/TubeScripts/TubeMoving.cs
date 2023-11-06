using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMoving : MonoBehaviour
{
    [SerializeField] private GameObject form;
    [SerializeField] private Camera puzzleCamera;

    private bool isMoving = false;
    private Vector3 mousePos;
    
    void Update()
    {
        PuzzleMoving();
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0)) {
            isMoving = true;
            mousePos = Input.mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;

        if (Mathf.Abs(this.transform.localPosition.x - form.transform.localPosition.x) <= 0.15f &&
            Mathf.Abs(this.transform.localPosition.y - form.transform.localPosition.y) <= 0.15f) {
            this.transform.position = new Vector2(form.transform.position.x, form.transform.position.y);
        }
    }

    private void PuzzleMoving() {
        if (isMoving) {
            mousePos = Input.mousePosition;
            mousePos = puzzleCamera.ScreenToWorldPoint(mousePos);
       
            this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, -0.62f);
        }
    }
}

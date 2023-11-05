using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeMoving : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 mousePos;
    
    void Update()
    {
        PuzzleMoving();
        //mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        //Debug.Log(mousePos);
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
    }

    private void PuzzleMoving() {
        if (isMoving) {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
       
            this.gameObject.transform.position = new Vector2(mousePos.x, mousePos.y);
        }
    }
}

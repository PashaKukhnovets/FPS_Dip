using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMoving : MonoBehaviour
{ 
    private bool isMoving = false;
    private Vector3 mousePos;

    void Update()
    {
        PuzzleMoving();
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(mousePos);
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            mousePos = Input.mousePosition;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;

        if ((mousePos.x >= -0.85f && mousePos.x <= 0.97) && (mousePos.y >= 0.45f && mousePos.y <= 2.75))
        {
            if (this.gameObject.CompareTag("RedWire"))
            {
                this.transform.position = new Vector3(mousePos.x, mousePos.y, 0.3f);
            }
            else if (this.gameObject.CompareTag("YellowWire"))
            {
                this.transform.position = new Vector3(mousePos.x, mousePos.y, 1.1f);
            }
            else if (this.gameObject.CompareTag("OrangeWire"))
            {
                this.transform.position = new Vector3(mousePos.x, mousePos.y, 1.5f);
            }
            else if (this.gameObject.CompareTag("GreenWire"))
            {
                this.transform.position = new Vector3(mousePos.x, mousePos.y, 0.7f);
            }
        }

        //if (Mathf.Abs(this.transform.localPosition.x - form.transform.localPosition.x) <= 0.15f &&
        //    Mathf.Abs(this.transform.localPosition.y - form.transform.localPosition.y) <= 0.15f)
        //{
        //    this.transform.position = new Vector2(form.transform.position.x, form.transform.position.y);
        //}
    }

    private void PuzzleMoving()
    {
        if (isMoving)
        {
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if ((mousePos.x >= -0.85f && mousePos.x <= 0.97) && (mousePos.y >= 0.45f && mousePos.y <= 2.75))
            {

                if (this.gameObject.CompareTag("RedWire"))
                {
                    this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0.3f);
                }
                else if (this.gameObject.CompareTag("YellowWire"))
                {
                    this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 1.1f);
                }
                else if (this.gameObject.CompareTag("OrangeWire"))
                {
                    this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 1.5f);
                }
                else if (this.gameObject.CompareTag("GreenWire"))
                {
                    this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 0.7f);
                }
            }
        }
    }
}

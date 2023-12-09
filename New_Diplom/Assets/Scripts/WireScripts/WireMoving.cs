using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireMoving : MonoBehaviour
{
    [SerializeField] private GameObject connectionPoint;
    [SerializeField] private Camera puzzleCamera;

    private bool isMoving = false;
    private Vector3 mousePos;
    private bool isFinishMoving = false;

    void Update()
    {
        PuzzleMoving();
        mousePos = Input.mousePosition;
        mousePos = puzzleCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnMouseDown()
    {
        if (!isFinishMoving)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isMoving = true;
                mousePos = Input.mousePosition;
            }
        }
    }

    private void OnMouseUp()
    {
        if (!isFinishMoving)
        {
            isMoving = false;

            if ((mousePos.x >= 0.0f && mousePos.x <= 1.86) && (mousePos.y >= 28.4f && mousePos.y <= 31.2))
            {
                if (this.gameObject.CompareTag("RedWire"))
                {
                    this.transform.position = new Vector3(mousePos.x, mousePos.y, 3.6f);
                }
                else if (this.gameObject.CompareTag("YellowWire"))
                {
                    this.transform.position = new Vector3(mousePos.x, mousePos.y, 4.3f);
                }
                else if (this.gameObject.CompareTag("OrangeWire"))
                {
                    this.transform.position = new Vector3(mousePos.x, mousePos.y, 4.6f);
                }
                else if (this.gameObject.CompareTag("GreenWire"))
                {
                    this.transform.position = new Vector3(mousePos.x, mousePos.y, 4.0f);
                }
            }

            if (Mathf.Abs(this.transform.localPosition.x - connectionPoint.transform.localPosition.x) <= 0.15f &&
                Mathf.Abs(this.transform.localPosition.y - connectionPoint.transform.localPosition.y) <= 0.15f) {

                if (this.gameObject.CompareTag("RedWire"))
                {
                    this.gameObject.transform.position = new Vector3(connectionPoint.transform.position.x, connectionPoint.transform.position.y, 3.6f);
                }
                else if (this.gameObject.CompareTag("YellowWire"))
                {
                    this.gameObject.transform.position = new Vector3(connectionPoint.transform.position.x, connectionPoint.transform.position.y, 4.3f);
                }
                else if (this.gameObject.CompareTag("OrangeWire"))
                {
                    this.gameObject.transform.position = new Vector3(connectionPoint.transform.position.x, connectionPoint.transform.position.y, 4.6f);
                }
                else if (this.gameObject.CompareTag("GreenWire"))
                {
                    this.gameObject.transform.position = new Vector3(connectionPoint.transform.position.x, connectionPoint.transform.position.y, 4.0f);
                }
                
                isFinishMoving = true;
            }
        }
    }

    private void PuzzleMoving()
    {
        if (!isFinishMoving)
        {
            if (isMoving)
            {
                mousePos = Input.mousePosition;
                mousePos = puzzleCamera.ScreenToWorldPoint(mousePos);

                if ((mousePos.x >= 0.0f && mousePos.x <= 1.86) && (mousePos.y >= 28.4f && mousePos.y <= 31.2))
                {

                    if (this.gameObject.CompareTag("RedWire"))
                    {
                        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 3.6f);
                    }
                    else if (this.gameObject.CompareTag("YellowWire"))
                    {
                        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 4.3f);
                    }
                    else if (this.gameObject.CompareTag("OrangeWire"))
                    {
                        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 4.6f);
                    }
                    else if (this.gameObject.CompareTag("GreenWire"))
                    {
                        this.gameObject.transform.position = new Vector3(mousePos.x, mousePos.y, 4.0f);
                    }
                }
            }
        }
    }
}

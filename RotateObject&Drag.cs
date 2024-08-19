using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeed = 30f;
    public float dragSpeed = 1f;

    private Vector3 dragOrigin;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            DragObject();
        }
        else
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }

    void DragObject()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePos = Input.mousePosition;
            Vector3 difference = currentMousePos - dragOrigin;
            difference = new Vector3(0, difference.y, 0);
            transform.position += difference * dragSpeed * Time.deltaTime;
            dragOrigin = currentMousePos;
        }
    }
}

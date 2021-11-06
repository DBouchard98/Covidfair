using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float xSensitivity;
    [SerializeField] float ySensitivity;

    private float xRotation = 0f;
    private float yRotation = 0f;

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (!Cursor.visible)
        {
            xRotation += Input.GetAxis("Mouse Y");
            yRotation += Input.GetAxis("Mouse X");

            xRotation = Mathf.Clamp(xRotation, -45f, 45f);

            transform.localEulerAngles = new Vector3(0, yRotation, 0) * xSensitivity;
            cam.transform.localEulerAngles = new Vector3(-xRotation, 0, 0) * ySensitivity;
        }

        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Cursor.visible && Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

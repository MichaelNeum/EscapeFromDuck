using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    private float yRotation = 0;
    public float controllerSensitivity = 500.0f;

    public Transform Player;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float yAxis = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        yRotation -= yAxis - GameController.yAxis / controllerSensitivity;
        yRotation = Mathf.Clamp(yRotation, -90, 90);
        if (GlobalData.PlayerData.alive)
        {
            transform.localRotation = Quaternion.Euler(yRotation, 0, 0);
            Player.Rotate(Vector3.up * (xAxis - GameController.xAxis / controllerSensitivity));
        }
    }
}

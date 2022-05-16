using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    private float xRotation = 0;

    public Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float xAxis = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        float yAxis = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation -= yAxis;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        if (GlobalData.PlayerData.alive)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            Player.Rotate(Vector3.up * xAxis);
        }   
    }
}

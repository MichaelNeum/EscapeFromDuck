using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class Movement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float moveForward = Mathf.Abs((GameController.forward + z)) > 1 ? z : (GameController.forward + z);

        Vector3 move = transform.right * x + transform.forward * moveForward;

        if (GlobalData.PlayerData.alive) controller.Move(move * speed * Time.deltaTime);
    }
}

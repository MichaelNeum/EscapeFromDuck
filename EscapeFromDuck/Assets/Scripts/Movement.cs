using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12;

    public float gravity = -9.81f;
    public float jumpHeight = 0.33f;

    float verticalVelocity;

    //variables for footsteps
    AudioSource audioSrc;
    Vector3 lastPosition;

    void Start()
    {
        Vector3 lastPosition = transform.position;
        audioSrc = GetComponent<AudioSource>();
    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        float moveForward = Mathf.Abs((GameController.forward + z)) > 1 ? z : (GameController.forward + z);

        // Increase gravity when falling down to make jump less "floaty"
        verticalVelocity += 3 * gravity * Time.deltaTime;
        if (controller.isGrounded) verticalVelocity = 0;

        if ((Input.GetKeyDown(KeyCode.Space) || GameController.jump) && controller.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
            GameController.jump = false;
        }

        Vector3 move = (transform.right * x + transform.forward * moveForward).normalized;

        if (GlobalData.PlayerData.alive)
        {
            controller.Move(move * speed * Time.deltaTime
                + new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
        }

        //footsteps
        Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
        

        if ((velocity.x != 0) || (velocity.z != 0))
        {
            if (!audioSrc.isPlaying)
            {
                audioSrc.Play();
            }
        } else
        {
            audioSrc.Stop();
        }

        lastPosition = transform.position;
    }

}

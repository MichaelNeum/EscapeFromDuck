using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class Movement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12;
    //variables for footsteps
    AudioSource audioSrc;
    Vector3 lastPosition;
    bool isMoving = false;

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

        Vector3 move = transform.right * x + transform.forward * moveForward;

        if (GlobalData.PlayerData.alive) controller.Move(move * speed * Time.deltaTime);

        //footsteps
        Vector3 velocity = (transform.position - lastPosition) / Time.deltaTime;
        if ((velocity.x != 0) || (velocity.z != 0))
        {
            isMoving = true;
        } else
        {
            isMoving = false;
        }

        if (isMoving)
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

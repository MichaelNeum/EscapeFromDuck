using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject duck;
    public float speed;

    public AudioSource source;
    public AudioClip clip;
    public float volume=0.01f;
    private bool hasPlayed = false;                  

    void Start()
    {
        duck = GameObject.FindGameObjectWithTag("Duck");
        GlobalData.PlayerData.alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 duckPosition = duck.transform.position;

        Vector3 direction = playerPosition - duckPosition;
        Vector3 movement = direction.normalized;

        // Zero out vertical movement to prevent duck from floating if player jumps
        movement.y = 0;

        float singleStep = speed * Time.deltaTime;
        duck.transform.position += movement * singleStep;

        Quaternion targetRotation = Quaternion.LookRotation(movement);
        //90 degree offset due to the duck model
        targetRotation *= Quaternion.Euler(0, 90, 0);
        //rotation
        duck.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime);
        float distance = direction.magnitude;
        
        if (distance < 20.0)
        {
            if(!hasPlayed)
            {
                source.PlayOneShot(clip, volume);   //play jumpscare sound               
                hasPlayed = true;                   //jumpscare sound has been already played
            }        
        }

        if (distance > 50)
        {
           hasPlayed = false;                  //reset jumpscare sound
        }

        if(distance < 2.0)
        {
            GlobalData.PlayerData.alive = false;
        }
    }
}

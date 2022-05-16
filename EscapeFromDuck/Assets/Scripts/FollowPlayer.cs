using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject duck;
    public float speed;
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
        duck.transform.position += direction.normalized * speed * Time.deltaTime;

        float distance = direction.magnitude;
        if(distance < 2.0)
        {
            GlobalData.PlayerData.alive = false;
        }
    }
}

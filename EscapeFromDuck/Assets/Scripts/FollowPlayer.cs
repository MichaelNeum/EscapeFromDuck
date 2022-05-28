using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject duck;

    public Movement cMovement;
    public ObjectInteraction cObjects;

    public float aggroDistance;
    public float killDistance;
    public float maxIdleTime = 60 * 5;
    private Vector3 currentDirection;
    private float lastDirectionChange = 0;
    public float directionChangeInterval;

    private Vector3 debugPlayerVector;

    // Jumpscare
    public AudioSource source;
    public AudioClip clip;
    public float volume = 0.01f;
    private bool hasPlayed = false;

    void OnDrawGizmos()
    {
        if (duck == null)
        {
            return;
        }

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(duck.transform.position, aggroDistance);

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(duck.transform.position, killDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(duck.transform.position, currentDirection * int.MaxValue);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(duck.transform.position, debugPlayerVector * int.MaxValue);
    }

    void Start()
    {
        duck = GameObject.FindGameObjectWithTag("Duck");
        GlobalData.PlayerData.alive = true;
    }

    void HandleJumpScare(float distance)
    {
        // Play jumpscare if it wasn't recently played and the player is in range
        if (distance < 20.0 && !hasPlayed)
        {
            hasPlayed = true;

            source.PlayOneShot(clip, volume);
        }

        // Reset jumpscare if the duck got away far enough
        if (distance > 50.0)
        {
            hasPlayed = false;
        }
    }

    Vector3 GetVectorToPlayer()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 duckPosition = duck.transform.position;

        return playerPosition - duckPosition;
    }

    Vector3 GetMovementVector(Vector3 player)
    {
        debugPlayerVector = player;

        bool shouldAggro = player.magnitude < aggroDistance || Time.timeSinceLevelLoad > maxIdleTime;

        // Zero out vertical plane to prevent duck from floating if player jumps
        player.y = 0;

        if (shouldAggro)
        {
            Vector3 direction = player.normalized;

            return currentDirection = direction;
        }
        else
        {
            bool shouldChangeDirection = (
                lastDirectionChange + directionChangeInterval < Time.timeSinceLevelLoad ||
                currentDirection == null
            );

            if (shouldChangeDirection)
            {
                lastDirectionChange = Time.timeSinceLevelLoad;

                // Randomly rotate the vector to the player by at most 90 degrees to either side using black magic
                float angle = Random.Range(270, 450) % 360;
                return currentDirection = (Quaternion.AngleAxis(angle, Vector3.up) * player).normalized;
            }
            else
            {
                return currentDirection;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 player = GetVectorToPlayer();
        Vector3 movement = GetMovementVector(player);

        float maxSpeed = cMovement.speed;
        float baseSpeed = maxSpeed * (2.0f / 3.0f);
        float speedScale = cObjects.CollectableCount / GlobalData.CollectableSpawn.numObjects;

        float scaledSpeed = baseSpeed + ((baseSpeed - maxSpeed) * speedScale);

        // Move the duck
        float singleStep = scaledSpeed * Time.deltaTime;
        duck.transform.position += movement * singleStep;

        if (movement != Vector3.zero)
        {
            // Rotate the duck in the direction of movement
            Quaternion targetRotation = Quaternion.LookRotation(movement);

            // 90 degree offset due to the duck model
            targetRotation *= Quaternion.Euler(0, 90, 0);
            duck.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 90 * Time.deltaTime);
        }

        // Project vector onto horizontal plane to get distance
        float distance = Vector3.ProjectOnPlane(player, Vector3.up).magnitude;

        if (distance < killDistance)
        {
            GlobalData.PlayerData.alive = false;
        }

        HandleJumpScare(distance);
    }
}

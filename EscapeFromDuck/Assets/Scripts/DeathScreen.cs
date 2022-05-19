using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject quitButton;
    public GameObject retryButton;

    void Start()
    {
        quitButton.SetActive(false);
        retryButton.SetActive(false);
    }

    void Update()
    {
        bool isDead = !GlobalData.PlayerData.alive;

        quitButton.SetActive(isDead);
        retryButton.SetActive(isDead);

        AudioListener.pause = isDead;

        if (isDead)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

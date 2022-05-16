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
        if(!GlobalData.PlayerData.alive)
        {
            quitButton.SetActive(true);
            retryButton.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            quitButton.SetActive(false);
            retryButton.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public GameObject quitButton;
    public GameObject retryButton;
    public GameObject deathText;
    public GameObject winText;
     
    void Start()
    {
        quitButton.SetActive(false);
        retryButton.SetActive(false);
        deathText.SetActive(false);
        winText.SetActive(false);
        GlobalData.PlayerData.won = false;
    }

    void Update()
    {
        bool isDead = !GlobalData.PlayerData.alive || GlobalData.PlayerData.won;

        quitButton.SetActive(isDead);
        retryButton.SetActive(isDead);
        deathText.SetActive(!GlobalData.PlayerData.alive);
        winText.SetActive(GlobalData.PlayerData.won);

        AudioListener.pause = isDead;

        if (isDead)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

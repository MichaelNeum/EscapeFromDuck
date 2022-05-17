using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class ButtonBehaivour : MonoBehaviour
{
    public void onClickRetry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void onClickQuit()
    {
        GameController.quit();
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class ButtonBehaivour : MonoBehaviour
{
    public void onClickRetry()
    {
        Application.LoadLevel(Application.loadedLevel);
        GameController.turnOffAllLed();
    }

    public void onClickQuit()
    {
        GameController.turnOffAllLed();
        GameController.quit();
        Application.Quit();
    }
}

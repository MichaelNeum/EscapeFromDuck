using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaivour : MonoBehaviour
{
    public void onClickRetry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void onClickQuit()
    {
        Application.Quit();
    }
}

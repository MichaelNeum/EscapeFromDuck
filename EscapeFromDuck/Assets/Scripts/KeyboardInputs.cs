using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ControllerSpace;

public class KeyboardInputs : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) GameController.connectController();
    }
}

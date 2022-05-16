using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light light;
    public Light dirLight;
    private bool changeDirLight = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            light.enabled = !light.enabled;
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            if (changeDirLight) dirLight.intensity = 1;
            else dirLight.intensity = 0.035f;
            changeDirLight = !changeDirLight;
        }
    }
}

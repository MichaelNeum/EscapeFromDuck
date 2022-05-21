using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ControllerSpace;

public class ObjectInteraction : MonoBehaviour
{
    public int CollectableCount = 0;
    
    private const int CollectableLayer = 9;
    private int MaxCollectable;
    public Text Progress;

    void Start()
    {
        MaxCollectable = GlobalData.CollectableSpawn.numObjects;
        UpdateProgress();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) || GameController.interaction)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 10f))
            {
                if (hit.collider.gameObject.layer == CollectableLayer)
                {
                    Destroy(hit.collider.gameObject);
                    GameController.turnOnLed(CollectableCount);
                    CollectableCount++;
                    UpdateProgress();
                }
            }
        }
    }

    private void UpdateProgress()
    {
        Progress.text = CollectableCount + " / " + MaxCollectable;
    }
}

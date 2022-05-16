using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData.CollectableSpawn;

public class SpawnCollectables : MonoBehaviour
{
    public GameObject collectable;
    void Start()
    {
        Vector3[] positions = getPositions();
        for(int i = 0; i < positions.Length; i++)
        {
            Instantiate(collectable, positions[i], Quaternion.identity);
        }
    }

    private Vector3[] getPositions()
    {
        Vector3[] result = new Vector3[numObjects];
        for(int i = 0; i < numObjects; i++)
        {
            Vector3 value = randomSpawnPoint();
            while(inArray(result, value))
            {
                value = randomSpawnPoint();
            }
            result[i] = value;
        }
        return result;
    }

    private bool inArray(Vector3[] vector, Vector3 element)
    {
        for(int i = 0; i < vector.Length; i++)
        {
            if (vector[i].Equals(element)) return true;
        }
        return false;
    }
}

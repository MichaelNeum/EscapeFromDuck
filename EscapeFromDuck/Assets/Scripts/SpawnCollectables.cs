﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData.CollectableSpawn;

public class SpawnCollectables : MonoBehaviour
{
    public GameObject collectable;
    public Vector3[] possibleSpawns;
    private int[] takenSpawns;
    private int items;
    private bool islandTwice;
    private bool cafeeTwice;
    public bool spawnAllCollectibles;

    void Start()
    {
        items = 4;
        bool islandTwice = false;
        bool cafeeTwice = false;
        spawnAllCollectibles = false;

        possibleSpawns = new Vector3[18];
        possibleSpawns[0] = new Vector3(-86f, 1.4f, -18.5f);
        possibleSpawns[1] = new Vector3(-102f, 1.4f, -100f);
        possibleSpawns[2] = new Vector3(-125f, 1.4f, -151f);
        possibleSpawns[3] = new Vector3(-43f, 1.4f, -205f);
        possibleSpawns[4] = new Vector3(46f, 1.4f, -190f);
        possibleSpawns[5] = new Vector3(-5f, 1.4f, -147.5f);
        possibleSpawns[6] = new Vector3(55f, 1.4f, -75f);
        //next three never together
        possibleSpawns[7] = new Vector3(-38f, 1.4f, -84f);
        possibleSpawns[8] = new Vector3(-27f, 1.4f, -69f);
        possibleSpawns[9] = new Vector3(-41f, 1.4f, -68f);

        possibleSpawns[10] = new Vector3(105f, 2.25f, -38f);
        possibleSpawns[11] = new Vector3(72f, 4f, 36f);
        possibleSpawns[12] = new Vector3(-17f, 6f, 80f);
        possibleSpawns[13] = new Vector3(-91.5f, 8f, 30f);
        //next three never together
        possibleSpawns[14] = new Vector3(-57f, 1.75f, -17f);
        possibleSpawns[15] = new Vector3(-21f, 1.75f, -17f);
        possibleSpawns[16] = new Vector3(-35f, 1.75f, -22f);

        possibleSpawns[17] = new Vector3(108f, 2.25f, -138f);

        if (spawnAllCollectibles)
        {
            spawnAll();
        } else
        {
            Vector3[] positions = getPositions();
            for (int i = 0; i < positions.Length; i++)
            {
                Instantiate(collectable, positions[i], Quaternion.identity);
            }
        }
    }

    private void spawnAll()
    {
        for (int i = 0; i < possibleSpawns.Length; i++)
        {
            Instantiate(collectable, possibleSpawns[i], Quaternion.identity);
        }
    }

    private Vector3[] getPositions()
    {
        takenSpawns = new int[items];

        for (int i = 0; i < items; i++)
        {
            int spawn = Random.Range(0, possibleSpawns.Length - 1);
            while (checkAlreadyTaken(spawn))
            {
                spawn = Random.Range(0, possibleSpawns.Length - 1);
            }
            takenSpawns[i] = spawn;
        }

        Vector3[] result = new Vector3[items];
        for (int i = 0; i < items; i++)
        {
            result[i] = possibleSpawns[takenSpawns[i]];
        }
        return result;
    }

    private bool checkAlreadyTaken(int check)
    {
        if (check == 7 || check == 8 || check == 9 || check == 14 || check == 15 || check == 16)
        {
            if (islandTwice)
            {
                while (check == 7 || check == 8 || check == 9)
                {
                    return true;
                }
            }

            if (check == 7 || check == 8 || check == 9)
            {
                islandTwice = true;
            }

            if (cafeeTwice)
            {
                while (check == 14 || check == 15 || check == 16)
                {
                    return true;
                }
            }

            if (check == 14 || check == 15 || check == 16)
            {
                cafeeTwice = true;
            }
        }

        foreach (int x in takenSpawns)
        {
            if (x == check)
            {
                return true;
            }
        }

        return false;
    }
}

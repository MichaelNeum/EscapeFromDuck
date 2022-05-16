using System;
using System.Collections;
using UnityEngine;

namespace GlobalData
{
    public class CollectableSpawn
    {
        private static readonly int _numObjects = 4;
        private static readonly Vector3[] _locations = {
            new Vector3(200, 1, 200),
            new Vector3(100, 1, 100),
            new Vector3(300, 1,  0),
            new Vector3(-100, 1, 0),
            new Vector3(0, 1, -200),
            new Vector3(-50, 1, -50),
            new Vector3(-10, 1, 10),
            new Vector3(22, 1, -12)
        };

        public static int numObjects { get { return _numObjects; } }

        public static Vector3 randomSpawnPoint() 
        {
            System.Random random = new System.Random();
            return _locations[random.Next(_locations.Length)];
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZoneImportantObjectSpawnData", menuName = "ScriptableObjects/ZoneImportantObjectSpawnData", order = 5)]
public class ImportantObjectSpawnData : ScriptableObject
{
    [Serializable]
    public struct SpawnData
    {
        public string spawnDataName;
        public GameObject spawnDataRef;
    }
    public SpawnData[] spawnData;
}

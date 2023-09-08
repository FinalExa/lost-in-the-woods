using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawnerIDAssign
{
    private SeedSpawner[] seedSpawners;
    private GameObject zoneObject;

    public SeedSpawnerIDAssign(GameObject zoneObj)
    {
        zoneObject = zoneObj;
        AssignIDToSpawners();
    }

    private void AssignIDToSpawners()
    {
        seedSpawners = zoneObject.GetComponentsInChildren<SeedSpawner>();
        if (seedSpawners.Length > 0)
        {
            for (int i = 0; i < seedSpawners.Length; i++)
            {
                seedSpawners[i].AssignID(i + 1);
            }
        }
    }
}

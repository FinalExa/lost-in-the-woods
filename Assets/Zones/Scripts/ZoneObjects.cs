using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ZoneObjects
{
    [HideInInspector] public Zone zoneRef;

    public List<ZoneImportantObject> zoneImportantObjects;
    [Serializable]
    public struct ImportantObjectData
    {
        public string spawnDataName;
        public Vector3 objectPosition;
        public Vector3 objectRotatorEulerAngles;
        public int plantSignalState;
    }

    public ZoneObjects(Zone zone)
    {
        zoneRef = zone;
        Initialize();
    }

    public void Initialize()
    {
        zoneImportantObjects = new List<ZoneImportantObject>();
    }

    public void UpdateImportantObjectsFromSave(List<ImportantObjectData> receivedImportantObjectData)
    {
        for (int i = zoneImportantObjects.Count - 1; i >= 0; i--)
        {
            GameObject objectRef = zoneImportantObjects[i].gameObject;
            zoneImportantObjects[i].destroyedByZone = true;
            zoneImportantObjects.RemoveAt(i);
            GameObject.Destroy(objectRef);
        }
        foreach (ImportantObjectData importantObjectData in receivedImportantObjectData)
        {
            GameObject objectToSpawn = GetObjectToSpawnReference(importantObjectData.spawnDataName);
            if (objectToSpawn != null)
            {
                GameObject spawnedObjectRef = GameObject.Instantiate(objectToSpawn, importantObjectData.objectPosition, Quaternion.identity, zoneRef.transform);
                ZoneImportantObject zoneImportantObject = spawnedObjectRef.GetComponent<ZoneImportantObject>();
                zoneImportantObject.rotator.transform.eulerAngles = importantObjectData.objectRotatorEulerAngles;
                if (zoneImportantObject.plantSignalSet != null) zoneImportantObject.plantSignalSet.SetPlantState(importantObjectData.plantSignalState);
            }
        }
    }

    private GameObject GetObjectToSpawnReference(string spawnDataName)
    {
        for (int i = 0; i < zoneRef.zoneImportantObjectSpawnData.spawnData.Length; i++)
        {
            if (spawnDataName == zoneRef.zoneImportantObjectSpawnData.spawnData[i].spawnDataName) return zoneRef.zoneImportantObjectSpawnData.spawnData[i].spawnDataRef;
        }
        return null;
    }

    public int RegisterImportantObject(ZoneImportantObject objectToRegister)
    {
        zoneImportantObjects.Add(objectToRegister);
        return zoneImportantObjects.IndexOf(objectToRegister);
    }

    public void RemoveImportantObject(int id)
    {
        if (id != -1 && zoneImportantObjects.Count > 0)
        {
            zoneImportantObjects.RemoveAt(id);
            for (int i = 0; i < zoneImportantObjects.Count; i++)
            {
                zoneImportantObjects[i].ChangeId(i);
            }
        }
    }

    public List<ImportantObjectData> GenerateImportantObjectData()
    {
        List<ImportantObjectData> importantObjectsData = new List<ImportantObjectData>();
        foreach (ZoneImportantObject zoneImportantObject in zoneImportantObjects)
        {
            ImportantObjectData importantObjectData = new ImportantObjectData();
            importantObjectData.spawnDataName = zoneImportantObject.spawnDataName;
            importantObjectData.objectPosition = zoneImportantObject.gameObject.transform.position;
            importantObjectData.objectRotatorEulerAngles = zoneImportantObject.rotator.transform.eulerAngles;
            if (zoneImportantObject.plantSignalSet != null) importantObjectData.plantSignalState = zoneImportantObject.plantSignalSet.currentState;
            else importantObjectData.plantSignalState = -1;
            importantObjectsData.Add(importantObjectData);
        }
        return importantObjectsData;
    }
}
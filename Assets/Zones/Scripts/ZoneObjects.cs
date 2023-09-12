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
        public int valueToSave;
        public bool savesParent;
        public List<string> parentPath;
        public bool savedWithNoZone;
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
                if (importantObjectData.savesParent) spawnedObjectRef.transform.parent = LoadParent(importantObjectData.parentPath, importantObjectData).transform;
                if (zoneImportantObject.saveIntValuesForSaveSystem != null)
                {
                    zoneImportantObject.saveIntValuesForSaveSystem.ValueToSave = importantObjectData.valueToSave;
                    zoneImportantObject.saveIntValuesForSaveSystem.SetValue();
                }
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
            if (zoneImportantObject.saveParent)
            {
                importantObjectData.savesParent = true;
                importantObjectData.parentPath = new List<string>();
                importantObjectData.parentPath = GenerateParentPath(zoneImportantObject.gameObject, importantObjectData);
            }
            if (zoneImportantObject.saveIntValuesForSaveSystem != null) importantObjectData.valueToSave = zoneImportantObject.saveIntValuesForSaveSystem.ValueToSave;
            else importantObjectData.valueToSave = -100;
            importantObjectsData.Add(importantObjectData);
        }
        return importantObjectsData;
    }

    private List<string> GenerateParentPath(GameObject receivedObject, ImportantObjectData importantObjectData)
    {
        List<string> resultPath = new List<string>();
        GameObject currentTarget = receivedObject.transform.parent.gameObject;
        bool isFinished = false;
        while (!isFinished)
        {
            if (currentTarget.GetComponent<Zone>() == null && currentTarget.transform.parent != null)
            {
                resultPath.Add(currentTarget.name);
                currentTarget = currentTarget.transform.parent.gameObject;
            }
            else
            {
                if (currentTarget.transform.parent == null) importantObjectData.savedWithNoZone = true;
                else importantObjectData.savedWithNoZone = false;
                isFinished = true;
            }
        }
        return resultPath;
    }
    private GameObject LoadParent(List<string> receivedLoadList, ImportantObjectData importantObjectData)
    {
        GameObject target = null;
        int index = receivedLoadList.Count - 1;
        if (!importantObjectData.savedWithNoZone) target = zoneRef.gameObject;
        else
        {
            target = GameObject.Find(receivedLoadList[index]);
            index--;
        }
        for (int i = index; i >= 0; i--)
        {
            target = target.gameObject.transform.Find(receivedLoadList[i]).gameObject;
        }
        return target;
    }
}
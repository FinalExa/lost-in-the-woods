using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public enum ZoneType { GENERIC_FOREST, MONSTROUS_FOREST, DENSE_FOREST, ABANDONED_VILLAGE, FOREST_CENTER, TEST }
    [SerializeField] private string zoneName;
    [SerializeField] private ZoneType thisZoneType;
    [SerializeField] private float colliderReactivationDelay = 1f;
    [SerializeField] private float zoneHeartbeatCooldown;
    [SerializeField] private float zoneHeartbeatDuration;
    [SerializeField] private GameObject zoneGroundParent;
    public ZonePuzzle zonePuzzle;
    private List<ZoneGround> zoneGrounds;
    private PCController playerRef;
    [System.Serializable]
    public struct ZoneImportantObjectsRefs
    {
        public GameObject objectRef;
        public Vector3 objectPos;
        public Vector3 objectRotatorEulerAngles;
    }
    [System.Serializable]
    public struct ZoneImportantObjects
    {
        public ZoneImportantObjectsRefs zoneImportantObjectRef;
        public GameObject zoneImportantObjectInstance;
    }
    public List<ZoneImportantObjects> zoneImportantObjects;
    public ZoneImportantObject[] zoneImportantObjectInstances;

    private void Awake()
    {
        zoneImportantObjectInstances = this.transform.GetComponentsInChildren<ZoneImportantObject>();
    }

    private void Start()
    {
        ZoneStartup();
    }

    public void SetPlayerInZone(Collider other)
    {
        if (playerRef == null) playerRef = other.gameObject.GetComponent<PCController>();
        playerRef.pcReferences.pcZoneManager.ChangePlayerZone(this);
        playerRef.pcReferences.heartbeat.ChangeHeartbeatCooldownAndDuration(zoneHeartbeatCooldown, zoneHeartbeatDuration);
        SetZoneColliders(false);
        zonePuzzle.PlayerHasEntered();
    }

    private void CallForImportantObjectRegistration()
    {
        foreach (ZoneImportantObject zoneImportantObjectInstance in zoneImportantObjectInstances) zoneImportantObjectInstance.ImportantObjectRegistration(this);
    }

    public void SetPlayerOutOfZone()
    {
        StartCoroutine(WaitToReactivateColliders());
    }

    private void ZoneStartup()
    {
        zoneGrounds = new List<ZoneGround>();
        GetZoneColliders();
        zonePuzzle.ZonePuzzleStartup(this);
        zoneImportantObjects = new List<ZoneImportantObjects>();
        CallForImportantObjectRegistration();
    }

    private void GetZoneColliders()
    {
        Collider[] zoneGroundsCollider = zoneGroundParent.GetComponentsInChildren<Collider>();
        foreach (Collider collider in zoneGroundsCollider)
        {
            ZoneGround zoneGround = collider.gameObject.AddComponent<ZoneGround>();
            zoneGround.SetZone(this);
            zoneGrounds.Add(zoneGround);
        }
    }

    private void SetZoneColliders(bool stateToSet)
    {
        foreach (ZoneGround zoneGround in zoneGrounds)
        {
            zoneGround.checkActivated = stateToSet;
        }
    }

    public void AddZoneGround(ZoneGround zoneGround)
    {
        zoneGrounds.Add(zoneGround);
        if (playerRef.pcReferences.pcZoneManager.GetCurrentZone() == this) zoneGround.checkActivated = true;
        else zoneGround.checkActivated = false;
    }
    public void RemoveZoneGround(ZoneGround zoneGround)
    {
        zoneGrounds.Remove(zoneGround);
    }

    private IEnumerator WaitToReactivateColliders()
    {
        yield return new WaitForSeconds(colliderReactivationDelay);
        SetZoneColliders(true);
    }

    public void LoadingImportantObjects(List<ZoneImportantObjects> zoneImportantObjects)
    {
        List<ZoneImportantObjectsRefs> zoneImportantObjectsRefs = new List<ZoneImportantObjectsRefs>();
        foreach (ZoneImportantObjects zoneImportantObject in zoneImportantObjects)
        {
            zoneImportantObjectsRefs.Add(zoneImportantObject.zoneImportantObjectRef);
        }
        ReloadZoneImportantObjects(zoneImportantObjectsRefs);
    }

    private void ReloadZoneImportantObjects(List<ZoneImportantObjectsRefs> newZoneImportantObjectsRefs)
    {
        foreach (ZoneImportantObjects objectInZone in zoneImportantObjects)
        {
            objectInZone.zoneImportantObjectInstance.GetComponent<ZoneImportantObject>().destroyedByZone = true;
            GameObject.Destroy(objectInZone.zoneImportantObjectInstance);
            zoneImportantObjects.Remove(objectInZone);
        }
        foreach (ZoneImportantObjectsRefs zoneImportantObjectsRef in newZoneImportantObjectsRefs)
        {
            ZoneImportantObjects zoneImportantObject = new ZoneImportantObjects();
            zoneImportantObject.zoneImportantObjectRef = zoneImportantObjectsRef;
            zoneImportantObject.zoneImportantObjectInstance = Instantiate(zoneImportantObjectsRef.objectRef, zoneImportantObjectsRef.objectPos, Quaternion.identity, this.gameObject.transform);
            zoneImportantObject.zoneImportantObjectInstance.GetComponent<ZoneImportantObject>().rotator.transform.eulerAngles = zoneImportantObjectsRef.objectRotatorEulerAngles;
            zoneImportantObjects.Add(zoneImportantObject);
        }
    }

    public int RegisterImportantObject(GameObject objectInstance, GameObject objectRef, GameObject rotator)
    {
        ZoneImportantObjects zoneImportantObject = new ZoneImportantObjects();
        zoneImportantObject.zoneImportantObjectInstance = objectInstance;
        zoneImportantObject.zoneImportantObjectRef.objectRef = objectRef;
        zoneImportantObject.zoneImportantObjectRef.objectPos = objectInstance.transform.position;
        zoneImportantObject.zoneImportantObjectRef.objectRotatorEulerAngles = rotator.transform.eulerAngles;
        zoneImportantObjects.Add(zoneImportantObject);
        return (zoneImportantObjects.IndexOf(zoneImportantObject));
    }

    public void UpdateImportantObject(GameObject objectInstance, GameObject rotator, int id)
    {
        ZoneImportantObjects zoneImportantObject = zoneImportantObjects[id];
        zoneImportantObject.zoneImportantObjectRef.objectPos = objectInstance.transform.position;
        zoneImportantObject.zoneImportantObjectRef.objectRotatorEulerAngles = rotator.transform.eulerAngles;
        zoneImportantObjects[id] = zoneImportantObject;
    }

    public void RemoveImportantObject(int id)
    {
        zoneImportantObjects.RemoveAt(id);
        for (int i = 0; i < zoneImportantObjects.Count; i++)
        {
            zoneImportantObjects[i].zoneImportantObjectInstance.GetComponent<ZoneImportantObject>().id = i;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneImportantObject : MonoBehaviour
{
    public string spawnDataName;
    public GameObject rotator;
    [HideInInspector] public bool destroyedByZone;
    [HideInInspector] public int id = -1;
    private Zone thisZone;

    private void Awake()
    {
        thisZone = this.gameObject.transform.GetComponentInParent<Zone>();
        ImportantObjectRegistration();
    }

    public void ImportantObjectRegistration()
    {
        if (thisZone != null && id == -1) id = thisZone.zoneObjects.RegisterImportantObject(this);
    }

    public void ChangeId(int newId)
    {
        id = newId;
    }

    public void SetRotator(Vector3 newRotatorAngles)
    {
        rotator.transform.eulerAngles = newRotatorAngles;
    }

    private void OnDisable()
    {
        ImportantObjectRemoval();
    }

    public void ImportantObjectRemoval()
    {
        if (thisZone != null && id != -1 && !destroyedByZone) thisZone.zoneObjects.RemoveImportantObject(id);
    }
}

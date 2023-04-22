using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneImportantObject : MonoBehaviour
{
    [SerializeField] private GameObject baseRef;
    public GameObject rotator;
    [HideInInspector] public bool destroyedByZone;
    [HideInInspector] public int id = -1;
    private Zone thisZone;


    public void ImportantObjectRegistration(Zone parentZone)
    {
        thisZone = parentZone;
        if (id == -1) id = thisZone.RegisterImportantObject(this.gameObject, baseRef, rotator);
        else thisZone.UpdateImportantObject(this.gameObject, rotator, id);
    }

    private void OnDisable()
    {
        ImportantObjectRemoval();
    }

    public void ImportantObjectRemoval()
    {
        if (thisZone != null && id != -1 && !destroyedByZone) thisZone.RemoveImportantObject(id);
    }
}

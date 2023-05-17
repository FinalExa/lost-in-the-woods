using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnstableTerrain : MonoBehaviour, ISendSignalToSelf
{
    private ZoneGround zoneGround;
    [SerializeField] private UnstableTerrain transformationReference;

    private void Start()
    {
        zoneGround = this.gameObject.GetComponent<ZoneGround>();
    }

    public void OnSignalReceived(GameObject source)
    {
        SwapTerrain();
    }

    private void SwapTerrain()
    {
        if (zoneGround != null) zoneGround.ZoneRef.RemoveZoneGround(zoneGround);
        UnstableTerrain objectToSpawn = Instantiate(transformationReference, this.transform.position, Quaternion.identity);
        if (zoneGround != null)
        {
            objectToSpawn.gameObject.AddComponent<ZoneGround>().SetZone(zoneGround.ZoneRef);
            ZoneGround zoneGroundOfNewObject = objectToSpawn.gameObject.GetComponent<ZoneGround>();
            zoneGround.ZoneRef.AddZoneGround(zoneGroundOfNewObject);
        }
        GameObject.Destroy(this.gameObject);
    }
}

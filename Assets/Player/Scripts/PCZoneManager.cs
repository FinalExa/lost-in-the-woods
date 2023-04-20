using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCZoneManager : MonoBehaviour
{
    private Zone currentZone;

    private void Start()
    {
        CheckStartingZone();
    }
    public void ChangePlayerZone(Zone zoneToSet)
    {
        if (currentZone != null) currentZone.SetPlayerOutOfZone();
        currentZone = zoneToSet;
    }

    public Zone GetCurrentZone()
    {
        return currentZone;
    }

    private void CheckStartingZone()
    {
        Collider[] collidersTouchingPlayerAtStart = Physics.OverlapBox(this.transform.position, new Vector3(0.1f, 2f, 0.1f));
        foreach (Collider collider in collidersTouchingPlayerAtStart)
        {
            ZoneGround zoneGround = collider.GetComponent<ZoneGround>();
            if (zoneGround != null) zoneGround.SetPlayerInZone(this.gameObject.GetComponent<Collider>());
        }
    }
}

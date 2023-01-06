using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    private Zone zoneRef;

    public void SetZone(Zone zone)
    {
        zoneRef = zone;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) zoneRef.SetPlayerInZone(other);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGround : MonoBehaviour
{
    private Zone zoneRef;
    [HideInInspector] public bool checkActivated = true;

    public void SetZone(Zone zone)
    {
        zoneRef = zone;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && checkActivated) zoneRef.SetPlayerInZone(other.collider);
    }

    public void SetPlayerInZone(Collider playerCollider)
    {
        zoneRef.SetPlayerInZone(playerCollider);
    }
}

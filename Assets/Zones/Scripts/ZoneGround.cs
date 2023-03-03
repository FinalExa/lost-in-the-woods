using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGround : MonoBehaviour
{
    public Zone ZoneRef { get; private set; }
    [HideInInspector] public bool checkActivated = true;

    public void SetZone(Zone zone)
    {
        ZoneRef = zone;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && checkActivated) ZoneRef.SetPlayerInZone(other.collider);
    }

    public void SetPlayerInZone(Collider playerCollider)
    {
        ZoneRef.SetPlayerInZone(playerCollider);
    }
}

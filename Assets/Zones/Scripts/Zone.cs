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

    private void Start()
    {
        ZoneStartup();
    }

    public void SetPlayerInZone(Collider other)
    {
        if (playerRef == null) playerRef = other.gameObject.GetComponent<PCController>();
        playerRef.ChangePlayerZone(this);
        playerRef.pcReferences.heartbeat.ChangeHeartbeatCooldownAndDuration(zoneHeartbeatCooldown, zoneHeartbeatDuration);
        SetZoneColliders(false);
        zonePuzzle.PlayerHasEntered();
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

    private IEnumerator WaitToReactivateColliders()
    {
        yield return new WaitForSeconds(colliderReactivationDelay);
        SetZoneColliders(true);
    }
}

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
    public ZonePuzzle zonePuzzle;
    private Collider[] zoneColliders;
    private bool playerIsInThisZone;
    private PCController playerRef;

    private void Start()
    {
        ZoneStartup();
    }

    private void SetPlayerInZone(Collider other)
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
        GetZoneColliders();
        zonePuzzle.zoneRef = this;
        zonePuzzle.ZonePuzzleStartup();
    }

    private void GetZoneColliders()
    {
        zoneColliders = this.gameObject.GetComponents<Collider>();
    }

    private void SetZoneColliders(bool stateToSet)
    {
        foreach (Collider collider in zoneColliders)
        {
            collider.enabled = stateToSet;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) SetPlayerInZone(other);
    }

    private IEnumerator WaitToReactivateColliders()
    {
        yield return new WaitForSeconds(colliderReactivationDelay);
        SetZoneColliders(true);
    }
}

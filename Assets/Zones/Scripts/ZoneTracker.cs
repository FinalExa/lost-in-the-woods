using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTracker : MonoBehaviour
{
    public Zone[] gameZones;
    [System.Serializable]
    public struct ZoneInformation
    {
        public string zoneId;
        public bool zoneCompleted;
        public Zone.ZoneImportantObjectsRefs zoneImportantObjectRefs;
    }
    public ZoneInformation[] zoneInformation;

    private void Awake()
    {
        gameZones = FindObjectsOfType<Zone>();
    }

    private void Start()
    {

    }

    private void CompileZoneInformation()
    {
        zoneInformation = new ZoneInformation[gameZones.Length];
        foreach (Zone zone in gameZones)
        {
        }
    }
}

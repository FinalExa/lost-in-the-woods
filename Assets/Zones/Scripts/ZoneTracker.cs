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
    }

    private void Awake()
    {
        gameZones = FindObjectsOfType<Zone>();
    }
}

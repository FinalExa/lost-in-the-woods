using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTracker : MonoBehaviour
{
    public Zone[] gameZones;
    [System.Serializable]
    public struct VisitedZoneInformation
    {
        public int zoneId;
        public List<ZoneObjects.ImportantObjectData> zoneImportantObjectData;
    }
    public List<VisitedZoneInformation> visitedZonesInformation;

    private void Awake()
    {
        gameZones = FindObjectsOfType<Zone>();
    }

    private void Start()
    {
        InitializeVisitedZoneInformation();
    }

    private void InitializeVisitedZoneInformation()
    {
        visitedZonesInformation = new List<VisitedZoneInformation>();
    }

    public void CompileZoneInformation()
    {
        visitedZonesInformation.Clear();
        for (int i = 0; i < gameZones.Length; i++)
        {
            if (gameZones[i].visitedByPlayer && GetIfZoneIsCompleted(gameZones[i]))
            {
                VisitedZoneInformation visitedZoneInformation = new VisitedZoneInformation();
                visitedZoneInformation.zoneId = i;
                visitedZoneInformation.zoneImportantObjectData = gameZones[i].zoneObjects.GenerateImportantObjectData();
                visitedZonesInformation.Add(visitedZoneInformation);
            }
        }
    }

    private bool GetIfZoneIsCompleted(Zone zone)
    {
        if ((!zone.zonePuzzle.zoneHasPuzzle) || (zone.zonePuzzle.zoneHasPuzzle && zone.zonePuzzle.puzzleDone)) return true;
        return false;
    }
}

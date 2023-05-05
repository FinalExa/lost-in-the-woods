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

    private void Awake()
    {
        gameZones = FindObjectsOfType<Zone>();
    }

    public void ApplyZoneInformation(List<VisitedZoneInformation> zoneInformationToApply)
    {
        for (int i = 0; i < zoneInformationToApply.Count; i++)
        {
            Zone zoneToOperate = gameZones[zoneInformationToApply[i].zoneId];
            zoneToOperate.visitedByPlayer = true;
            zoneToOperate.zonePuzzle.puzzleDone = true;
            zoneToOperate.zoneObjects.UpdateImportantObjectsFromSave(zoneInformationToApply[i].zoneImportantObjectData);
        }
    }

    public List<VisitedZoneInformation> CompileZoneInformation()
    {
        List<VisitedZoneInformation> visitedZonesInformation = new List<VisitedZoneInformation>();
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
        return visitedZonesInformation;
    }

    private bool GetIfZoneIsCompleted(Zone zone)
    {
        if ((!zone.zonePuzzle.zoneHasPuzzle) || (zone.zonePuzzle.zoneHasPuzzle && zone.zonePuzzle.puzzleDone)) return true;
        return false;
    }
}

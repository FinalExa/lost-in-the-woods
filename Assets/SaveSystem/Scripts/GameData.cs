using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public List<ZoneTracker.VisitedZoneInformation> visitedZonesInformation;

    public GameData(Vector3 playerPos, List<ZoneTracker.VisitedZoneInformation> visitedZones)
    {
        SetGameData(playerPos, visitedZones);
    }


    public void SetGameData(Vector3 playerPos, List<ZoneTracker.VisitedZoneInformation> visitedZones)
    {
        playerPosition = playerPos;
        visitedZonesInformation = visitedZones;
    }
}

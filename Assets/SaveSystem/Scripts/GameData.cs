using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public string sceneName;
    public Vector3 playerPosition;
    public List<ZoneTracker.VisitedZoneInformation> visitedZonesInformation;

    public GameData(string receivedSceneName, Vector3 playerPos, List<ZoneTracker.VisitedZoneInformation> visitedZones)
    {
        SetGameData(receivedSceneName, playerPos, visitedZones);
    }


    public void SetGameData(string receivedSceneName, Vector3 playerPos, List<ZoneTracker.VisitedZoneInformation> visitedZones)
    {
        sceneName = receivedSceneName;
        playerPosition = playerPos;
        visitedZonesInformation = visitedZones;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    [Serializable]
    public struct MapSave
    {
        public Zone zone;
        public GameObject zoneStateObject;
    }
    public List<MapSave> mapSave;

    public GameData(Vector3 playerPos, Zone[] zones)
    {
        SetGameData(playerPos, zones);
    }


    public void SetGameData(Vector3 playerPos, Zone[] zones)
    {
        playerPosition = playerPos;
        if (zones.Length > 0) SetZones(zones);
    }

    private void SetZones(Zone[] zones)
    {
        mapSave = new List<MapSave>();
        foreach (Zone zone in zones)
        {
            MapSave singleZone = new MapSave();
            singleZone.zone = zone;
            singleZone.zoneStateObject = zone.gameObject;
            mapSave.Add(singleZone);
        }
    }
}

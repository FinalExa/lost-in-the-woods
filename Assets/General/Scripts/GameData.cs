using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameData
{
    public Vector3 playerPosition;
    [Serializable]
    public struct ZoneSave
    {
        public string zoneId;
        public bool zone;
        public GameObject zoneStateObject;
    }
    public ZoneSave[] zoneSave;

    public GameData(Vector3 playerPos)
    {
        //zoneSave = new ZoneSave[zones.Length];
        SetGameData(playerPos);
    }


    public void SetGameData(Vector3 playerPos)
    {
        playerPosition = playerPos;
        //if (zones.Length > 0) SetZones(zones);
    }

    private void SetZones(Zone[] zones)
    {
        /*mapSave = new List<MapSave>();
        foreach (Zone zone in zones)
        {
            MapSave singleZone = new MapSave();
            singleZone.zone = zone;
            singleZone.zoneStateObject = zone.gameObject;
            mapSave.Add(singleZone);
        }*/
    }
}

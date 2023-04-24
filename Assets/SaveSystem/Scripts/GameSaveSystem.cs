using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameSaveSystem : MonoBehaviour
{
    private string path = string.Empty;
    private string persistentPath = string.Empty;
    public PCController playerRef;
    public GameData gameData;
    private ZoneTracker zoneTracker;

    private void Awake()
    {
        playerRef = FindObjectOfType<PCController>();
        zoneTracker = this.gameObject.GetComponent<ZoneTracker>();
    }

    private void Start()
    {
        gameData = new GameData(playerRef.transform.position, new List<ZoneTracker.VisitedZoneInformation>());
        SetPaths();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }


    public void SaveData(Vector3 playerPosition)
    {
        gameData.SetGameData(playerPosition, zoneTracker.CompileZoneInformation());
        string savePath = path;

        string json = JsonUtility.ToJson(gameData);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        GameData dataToApply = JsonUtility.FromJson<GameData>(json);
        if (dataToApply != null) ApplyLoadedData(dataToApply);
    }

    private void ApplyLoadedData(GameData dataToApply)
    {
        gameData = dataToApply;
        playerRef.transform.position = gameData.playerPosition;
        playerRef.pcReferences.heartbeat.SetHeartbeatTimer(false);
        playerRef.pcReferences.pcZoneManager.GetCurrentZone().TurnOffAllEnemiesInZone();
        zoneTracker.ApplyZoneInformation(gameData.visitedZonesInformation);
    }
}

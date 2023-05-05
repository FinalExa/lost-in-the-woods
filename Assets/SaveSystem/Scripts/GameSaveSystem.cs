using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        gameData = new GameData(SceneManager.GetActiveScene().name, playerRef.transform.position, new List<ZoneTracker.VisitedZoneInformation>());
        SetPaths();
        LoadData();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }


    public void SaveData(Vector3 playerPosition)
    {
        gameData.SetGameData(SceneManager.GetActiveScene().name, playerPosition, zoneTracker.CompileZoneInformation());
        string savePath = path;

        string json = JsonUtility.ToJson(gameData);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        if (File.Exists(path))
        {
            using StreamReader reader = new StreamReader(path);
            string json = reader.ReadToEnd();
            GameData dataToApply = JsonUtility.FromJson<GameData>(json);
            if (dataToApply != null && dataToApply.sceneName == SceneManager.GetActiveScene().name) ApplyLoadedData(dataToApply);
        }
    }

    private void ApplyLoadedData(GameData dataToApply)
    {
        gameData = dataToApply;
        playerRef.transform.position = gameData.playerPosition;
        playerRef.pcReferences.heartbeat.SetHeartbeatTimer(false);
        if (playerRef.pcReferences.pcZoneManager.GetCurrentZone() != null) playerRef.pcReferences.pcZoneManager.GetCurrentZone().TurnOffAllEnemiesInZone();
        zoneTracker.ApplyZoneInformation(gameData.visitedZonesInformation);
    }

    public void DeleteLoadedData()
    {
        if (File.Exists(path)) File.Delete(path);
    }
}
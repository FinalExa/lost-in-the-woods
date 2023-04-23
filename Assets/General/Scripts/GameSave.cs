using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class GameSave : MonoBehaviour
{
    private string path = string.Empty;
    private string persistentPath = string.Empty;
    public PCController playerRef;
    public GameData gameData;

    private void Awake()
    {
        playerRef = FindObjectOfType<PCController>();
        SetZoneObjectsTracker();
    }

    private void SetZoneObjectsTracker()
    {
    }

    private void Start()
    {
        gameData = new GameData(playerRef.transform.position);
        SetPaths();
        //LoadData();
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }


    public void SaveData()
    {
        gameData.SetGameData(playerRef.transform.position);
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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameSave : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    private string path = string.Empty;
    private string persistentPath = string.Empty;

    private void Start()
    {
        SetPaths();
    }
    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }


    public void SaveData()
    {
        string savePath = path;

        print("Saving data at " + savePath);
        string json = JsonUtility.ToJson(gameData);
        print(json);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public void LoadData()
    {
        using StreamReader reader = new StreamReader(path);
        string json = reader.ReadToEnd();

        JsonUtility.FromJsonOverwrite(json, gameData);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    [SerializeField] private GameData gameData;

    private void Start()
    {
        Load();
    }
    public void Save()
    {
        JsonUtility.ToJson(gameData);
    }

    public void Load()
    {
        gameData = JsonUtility.FromJson<GameData>("gameData");
        print(gameData.count);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private Zone thisZone;
    private GameSaveSystem gameSave;

    private void Awake()
    {
        thisZone = this.transform.GetComponentInParent<Zone>();
        gameSave = FindObjectOfType<GameSaveSystem>();
    }
    public void Save()
    {
        if (thisZone != null && !thisZone.zonePuzzle.puzzleActive) gameSave.SaveData(this.transform.position);
    }
}

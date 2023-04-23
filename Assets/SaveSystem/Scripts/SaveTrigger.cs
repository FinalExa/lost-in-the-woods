using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    private Zone thisZone;
    private GameSave gameSave;

    private void Awake()
    {
        thisZone = this.transform.GetComponentInParent<Zone>();
        gameSave = FindObjectOfType<GameSave>();
    }
    public void Save()
    {
        if (thisZone != null && !thisZone.zonePuzzle.puzzleActive) gameSave.SaveData(this.transform.position);
    }
}

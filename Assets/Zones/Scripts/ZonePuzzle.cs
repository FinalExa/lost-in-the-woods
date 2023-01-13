using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZonePuzzle
{
    [HideInInspector] public Zone zoneRef;
    [SerializeField] private bool zoneHasPuzzle;
    private bool puzzleDone;
    private bool puzzleActive;
    [SerializeField] private GameObject puzzleActiveParent;
    [SerializeField] private GameObject puzzleInactiveParent;
    [SerializeField] private GameObject puzzleExit;

    public void ZonePuzzleStartup(Zone zone)
    {
        zoneRef = zone;
        if (zoneHasPuzzle)
        {
            puzzleInactiveParent.SetActive(false);
            puzzleActiveParent.SetActive(false);
            PuzzleExit puzzleExitRef = puzzleExit.AddComponent<PuzzleExit>();
            puzzleExitRef.zoneRef = zoneRef;
        }
    }

    public void PlayerHasEntered()
    {
        if (zoneHasPuzzle && !puzzleDone)
        {
            puzzleActiveParent.SetActive(true);
            puzzleActive = true;
        }
    }

    public void ZonePuzzleEnd()
    {
        if (zoneHasPuzzle)
        {
            puzzleActiveParent.SetActive(false);
            puzzleInactiveParent.SetActive(true);
            puzzleDone = true;
            puzzleActive = false;
        }
    }
}

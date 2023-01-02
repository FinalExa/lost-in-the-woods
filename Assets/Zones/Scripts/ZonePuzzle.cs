using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZonePuzzle
{
    [HideInInspector] public Zone zoneRef;
    [SerializeField] private bool zoneHasPuzzle;
    private bool puzzleDone;
    [SerializeField] private GameObject puzzleActiveParent;
    [SerializeField] private GameObject puzzleInactiveParent;
    [SerializeField] private GameObject puzzleEntrance;
    [SerializeField] private GameObject puzzleExit;

    public void ZonePuzzleStartup()
    {
        if (zoneHasPuzzle)
        {
            puzzleInactiveParent.SetActive(false);
            puzzleEntrance.SetActive(false);
            puzzleActiveParent.SetActive(true);
            PuzzleExit puzzleExitRef = puzzleExit.AddComponent<PuzzleExit>();
            puzzleExitRef.zoneRef = zoneRef;
        }
    }

    public void PlayerHasEntered()
    {
        if (zoneHasPuzzle) puzzleEntrance.SetActive(true);
    }

    public void ZonePuzzleEnd()
    {
        if (zoneHasPuzzle)
        {
            puzzleActiveParent.SetActive(false);
            puzzleEntrance.SetActive(false);
            puzzleInactiveParent.SetActive(true);
            puzzleDone = true;
        }
    }
}

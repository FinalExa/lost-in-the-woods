using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ZonePuzzle
{
    [HideInInspector] public Zone zoneRef;
    public bool zoneHasPuzzle;
    public bool puzzleDone;
    [HideInInspector] public bool puzzleActive;
    [Header("You must fill these with GameObjects.")]
    [SerializeField] private GameObject puzzleActiveParent;
    [SerializeField] private GameObject puzzleInactiveParent;
    [SerializeField] private GameObject puzzleExit;
    [Header("Filling these is not mandatory, do it if you want to activate/deactivate stuff when the puzzle ends.")]
    [SerializeField] private GameObject activateOnPuzzleEnd;
    [SerializeField] private GameObject deactivateOnPuzzleEnd;
    public static Action<Zone, GameObject> deactivatingPuzzleObject;

    public void ZonePuzzleStartup(Zone zone)
    {
        zoneRef = zone;
        if (zoneHasPuzzle)
        {
            puzzleInactiveParent.SetActive(false);
            puzzleActiveParent.SetActive(false);
            if (activateOnPuzzleEnd != null) activateOnPuzzleEnd.SetActive(false);
            if (deactivateOnPuzzleEnd != null) deactivateOnPuzzleEnd.SetActive(true);
            PuzzleExit puzzleExitRef = puzzleExit.AddComponent<PuzzleExit>();
            puzzleExitRef.zoneRef = zoneRef;
        }
    }

    public void PlayerHasEntered()
    {
        if (zoneHasPuzzle)
        {
            if (!puzzleDone)
            {
                puzzleInactiveParent.SetActive(false);
                puzzleActiveParent.SetActive(true);
                puzzleActive = true;
            }
            else
            {
                puzzleActiveParent.SetActive(false);
                puzzleInactiveParent.SetActive(true);
            }
        }
    }

    public void ZonePuzzleEnd()
    {
        if (zoneHasPuzzle)
        {
            if (deactivatingPuzzleObject != null) deactivatingPuzzleObject(zoneRef, puzzleActiveParent);
            puzzleActiveParent.SetActive(false);
            puzzleInactiveParent.SetActive(true);
            if (activateOnPuzzleEnd != null) activateOnPuzzleEnd.SetActive(true);
            if (deactivateOnPuzzleEnd != null)
            {
                if (deactivatingPuzzleObject != null) deactivatingPuzzleObject(zoneRef, deactivateOnPuzzleEnd);
                deactivateOnPuzzleEnd.SetActive(false);
            }
            puzzleDone = true;
            puzzleActive = false;
        }
    }
}

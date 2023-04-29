using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleExit : MonoBehaviour
{
    [HideInInspector] public Zone zoneRef;

    private void OnDisable()
    {
        if (zoneRef.zonePuzzle.puzzleActive) zoneRef.zonePuzzle.ZonePuzzleEnd();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPillar : MonoBehaviour
{
    [SerializeField] private bool lightEnabled;
    [SerializeField] private GameObject lightObj;
    [SerializeField] private bool corruptionEnabled;
    [SerializeField] private GameObject corruptionObj;
    [SerializeField] private GameObject seedSpace;
    private Seed seedRef;
    private int savedSignal;
    private int savedExploration;

    private void Start()
    {
        SetObjects(false, false);
    }

    public void SetSeed(Seed seed)
    {
        seedRef = seed;
        savedSignal = seed.signalState;
        savedExploration = seed.explorationState;
        SetSignals();
        seedRef.gameObject.transform.parent = seedSpace.transform;
        seedRef.gameObject.transform.localPosition = Vector3.zero;
        seedRef.LockRb();
    }

    public void RemoveSeed()
    {
        seedRef.signalState = savedSignal;
        savedSignal = 1;
        seedRef.explorationState = savedExploration;
        savedExploration = 1;
        SetSignals();
        seedRef.UnlockRb();
    }

    private void SetSignals()
    {
        if (savedSignal == 2) SetObjects(true, false);
        else if (savedSignal == 0) SetObjects(false, true);
        else SetObjects(false, false);
    }

    private void SetObjects(bool lightObjState, bool corruptionObjState)
    {
        lightObj.SetActive(lightObjState);
        corruptionObj.SetActive(corruptionObjState);
    }
}

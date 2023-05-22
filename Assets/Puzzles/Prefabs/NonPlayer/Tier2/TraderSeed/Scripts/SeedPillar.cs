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
        if (savedSignal == 1) SetObjects(true, false);
        else if (savedSignal == -1) SetObjects(false, true);
        else SetObjects(false, false);
        seedRef.gameObject.transform.parent = seedSpace.transform;
        seedRef.gameObject.transform.localPosition = Vector3.zero;
    }

    private void SetObjects(bool lightObjState, bool corruptionObjState)
    {
        lightObj.SetActive(lightObjState);
        corruptionObj.SetActive(corruptionObjState);
    }
}

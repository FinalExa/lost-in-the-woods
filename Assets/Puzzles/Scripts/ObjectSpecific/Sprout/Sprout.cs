using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private string puritySignalName;
    [SerializeField] private string corruptionSignalName;
    [SerializeField] private string extendRootName;
    [SerializeField] private string expandRootName;
    [SerializeField] private string[] fullyLockNames;
    [SerializeField] private GameObject baseSpike;
    [SerializeField] private SproutRootReceiver baseSpikeReceiver;
    [SerializeField] private GameObject[] extendedObjects;
    [SerializeField] private SproutRootReceiver extendedSpikeReceiver;
    [SerializeField] private GameObject[] expandedObjects;
    [SerializeField] private SproutRootReceiver expandedSpikeReceiver;
    [SerializeField] private GameObject[] purityObjects;
    [SerializeField] private GameObject[] corruptionObjects;
    private Interaction thisInteraction;
    private bool fullyLocked;
    private bool extendLocked;
    private bool expandLocked;

    private void Awake()
    {
        thisInteraction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        SetCurrentSproutRootStatus();
    }

    public void OnSignalReceived(GameObject source)
    {
        SproutRootOperations();
    }

    private void SproutRootOperations()
    {
        SetCurrentSproutRootStatus();
    }

    private void CheckLocks()
    {
        fullyLocked = !baseSpikeReceiver.GetStatus();
        extendLocked = !extendedSpikeReceiver.GetStatus();
        expandLocked = !expandedSpikeReceiver.GetStatus();
    }

    private void SetCurrentSproutRootStatus()
    {
        CheckLocks();
        PlantShapeStatus();
        ClearingStatus();
    }

    private void PlantShapeStatus()
    {
        if (!fullyLocked && !extendLocked && NameStatus(extendRootName) && !NameStatus(expandRootName)) SetShape(true, true, false);
        else if (!fullyLocked && !expandLocked && !NameStatus(extendRootName) && NameStatus(expandRootName)) SetShape(false, false, true);
        else SetShape(true, false, false);
    }

    private void SetShape(bool baseSpikeShape, bool extendedShape, bool expandedShape)
    {
        baseSpike.SetActive(baseSpikeShape);
        ArraySetActiveStatusOfObjects(extendedObjects, extendedShape);
        ArraySetActiveStatusOfObjects(expandedObjects, expandedShape);
    }

    private void ClearingStatus()
    {
        if (!fullyLocked && NameStatus(puritySignalName) && !NameStatus(corruptionSignalName)) SetSignals(true, false);
        else if (!fullyLocked && !NameStatus(puritySignalName) && NameStatus(corruptionSignalName)) SetSignals(false, true);
        else SetSignals(false, false);
    }

    private void SetSignals(bool puritySignal, bool corruptionSignal)
    {
        ArraySetActiveStatusOfObjects(purityObjects, puritySignal);
        ArraySetActiveStatusOfObjects(corruptionObjects, corruptionSignal);
    }

    private void ArraySetActiveStatusOfObjects(GameObject[] receivedArray, bool activeStatus)
    {
        foreach (GameObject receivedObject in receivedArray) receivedObject.SetActive(activeStatus);
    }

    private bool NameStatus(string nameToCheck)
    {
        return thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(nameToCheck);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutRoot : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private string lampPlantName;
    [SerializeField] private string corruptionPlantName;
    [SerializeField] private string guidingLightPlantName;
    [SerializeField] private string darkMistPlantName;
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
    private bool lampPlantInParent;
    private bool corruptionPlantInParent;
    private bool guidingLightPlantInParent;
    private bool darkMistPlantInParent;
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

    public void ReceiveSignalFromParent(bool lampPlantParent, bool corruptionPlantParent, bool guidingLightPlantParent, bool darkMistPlantParent)
    {
        lampPlantInParent = lampPlantParent;
        corruptionPlantInParent = corruptionPlantParent;
        guidingLightPlantInParent = guidingLightPlantParent;
        darkMistPlantInParent = darkMistPlantParent;
        SetCurrentSproutRootStatus();
    }

    private void SetCurrentSproutRootStatus()
    {
        CheckLocks();
        PlantShapeStatus();
        ClearingStatus();
    }

    private void PlantShapeStatus()
    {
        if (!fullyLocked && !extendLocked && (NameStatus(guidingLightPlantName) || guidingLightPlantInParent) && !(NameStatus(darkMistPlantName) || darkMistPlantInParent)) SetShape(true, true, false);
        else if (!fullyLocked && !expandLocked && !(NameStatus(guidingLightPlantName) || guidingLightPlantInParent) && (NameStatus(darkMistPlantName) || darkMistPlantInParent)) SetShape(false, false, true);
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
        if (!fullyLocked && (NameStatus(lampPlantName) || lampPlantInParent) && !(NameStatus(corruptionPlantName) || corruptionPlantInParent)) SetSignals(true, false);
        else if (!fullyLocked && !(NameStatus(lampPlantName) || lampPlantInParent) && (NameStatus(corruptionPlantName) || corruptionPlantInParent)) SetSignals(false, true);
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

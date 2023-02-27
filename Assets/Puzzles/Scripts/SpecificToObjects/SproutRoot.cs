using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SproutRoot : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private string lampPlantName;
    [SerializeField] private string corruptionPlantName;
    [SerializeField] private string guidingLightPlantName;
    [SerializeField] private string darkMistPlantName;
    [SerializeField] private GameObject baseSpike;
    [SerializeField] private GameObject[] extendedObjects;
    [SerializeField] private GameObject[] expandedObjects;
    [SerializeField] private GameObject[] purityObjects;
    [SerializeField] private GameObject[] corruptionObjects;
    private Interaction thisInteraction;
    private bool lampPlantIn;
    private bool lampPlantInParent;
    private bool corruptionPlantIn;
    private bool corruptionPlantInParent;
    private bool guidingLightPlantIn;
    private bool guidingLightPlantInParent;
    private bool darkMistPlantIn;
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
        CheckPlantStatus();
    }

    private void CheckPlantStatus()
    {
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(lampPlantName)) lampPlantIn = true;
        else lampPlantIn = false;
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(corruptionPlantName)) corruptionPlantIn = true;
        else corruptionPlantIn = false;
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(guidingLightPlantName)) guidingLightPlantIn = true;
        else guidingLightPlantIn = false;
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(darkMistPlantName)) darkMistPlantIn = true;
        else darkMistPlantIn = false;
        SetCurrentSproutRootStatus();
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
        PlantShapeStatus();
        ClearingStatus();
    }

    private void PlantShapeStatus()
    {
        if (!fullyLocked && !extendLocked && (guidingLightPlantIn || guidingLightPlantInParent) && !(darkMistPlantIn || darkMistPlantInParent))
        {
            baseSpike.SetActive(true);
            ArraySetActiveStatusOfObjects(extendedObjects, true);
            ArraySetActiveStatusOfObjects(expandedObjects, false);
        }
        else if (!fullyLocked && !expandLocked && !(guidingLightPlantIn || guidingLightPlantInParent) && (darkMistPlantIn || darkMistPlantInParent))
        {
            ArraySetActiveStatusOfObjects(expandedObjects, true);
            baseSpike.SetActive(false);
            ArraySetActiveStatusOfObjects(extendedObjects, false);
        }
        else
        {
            baseSpike.SetActive(true);
            ArraySetActiveStatusOfObjects(extendedObjects, false);
            ArraySetActiveStatusOfObjects(expandedObjects, false);
        }
    }

    private void ClearingStatus()
    {
        if (!fullyLocked && (lampPlantIn || lampPlantInParent) && !(corruptionPlantIn || corruptionPlantInParent))
        {
            ArraySetActiveStatusOfObjects(purityObjects, true);
            ArraySetActiveStatusOfObjects(corruptionObjects, false);
        }
        else if (!fullyLocked && !(lampPlantIn || lampPlantInParent) && (corruptionPlantIn || corruptionPlantInParent))
        {
            ArraySetActiveStatusOfObjects(purityObjects, false);
            ArraySetActiveStatusOfObjects(corruptionObjects, true);
        }
        else
        {
            ArraySetActiveStatusOfObjects(purityObjects, false);
            ArraySetActiveStatusOfObjects(corruptionObjects, false);
        }
    }

    private void ArraySetActiveStatusOfObjects(GameObject[] receivedArray, bool activeStatus)
    {
        foreach (GameObject receivedObject in receivedArray) receivedObject.SetActive(activeStatus);
    }
}

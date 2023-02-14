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
        if ((guidingLightPlantIn || guidingLightPlantInParent) && !(darkMistPlantIn || darkMistPlantInParent))
        {
            baseSpike.SetActive(true);
            foreach (GameObject obj in extendedObjects) obj.SetActive(true);
        }
        else if (!(guidingLightPlantIn || guidingLightPlantInParent) && (darkMistPlantIn || darkMistPlantInParent))
        {
            baseSpike.SetActive(false);
            foreach (GameObject obj in extendedObjects) obj.SetActive(false);
        }
        else
        {
            baseSpike.SetActive(true);
            foreach (GameObject obj in extendedObjects) obj.SetActive(false);
        }
    }

    private void ClearingStatus()
    {
        if ((lampPlantIn || lampPlantInParent) && !(corruptionPlantIn || corruptionPlantInParent))
        {
            foreach (GameObject obj in purityObjects) obj.SetActive(true);
            foreach (GameObject obj in corruptionObjects) obj.SetActive(false);
        }
        else if (!(lampPlantIn || lampPlantInParent) && (corruptionPlantIn || corruptionPlantInParent))
        {
            foreach (GameObject obj in purityObjects) obj.SetActive(false);
            foreach (GameObject obj in corruptionObjects) obj.SetActive(true);
        }
        else
        {
            foreach (GameObject obj in purityObjects) obj.SetActive(false);
            foreach (GameObject obj in corruptionObjects) obj.SetActive(false);
        }
    }
}

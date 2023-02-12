using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private SproutRoot[] sproutRoots;
    [SerializeField] private string lampPlantName;
    [SerializeField] private string corruptionPlantName;
    [SerializeField] private string guidingLightPlantName;
    [SerializeField] private string darkMistPlantName;
    [SerializeField] private Interaction thisInteraction;
    private bool lampPlantIn;
    private bool corruptionPlantIn;
    private bool guidingLightPlantIn;
    private bool darkMistPlantIn;

    private void Start()
    {
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
        SetSproutRoots();
    }

    private void SetSproutRoots()
    {
        if (sproutRoots.Length > 0)
        {
            foreach (SproutRoot sproutRoot in sproutRoots) sproutRoot.ReceiveSignalFromParent(lampPlantIn, corruptionPlantIn, guidingLightPlantIn, darkMistPlantIn);
        }
    }
}

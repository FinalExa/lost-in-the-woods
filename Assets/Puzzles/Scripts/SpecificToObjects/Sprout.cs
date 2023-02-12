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
        SetInvisibilityStatus();
    }

    private void SetInvisibilityStatus()
    {
        /*if (lightBulbIn && !fogPlantIn) objectToOperate.SetActive(true);
        else if (!lightBulbIn && fogPlantIn) objectToOperate.SetActive(false);
        else objectToOperate.SetActive(baseActiveState);*/
    }
}

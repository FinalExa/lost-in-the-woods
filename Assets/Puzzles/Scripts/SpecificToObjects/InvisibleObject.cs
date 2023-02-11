using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleObject : MonoBehaviour, ISendSignalToSelf
{
    [SerializeField] private bool baseActiveState;
    [SerializeField] private GameObject objectToOperate;
    [SerializeField] private string lightBulbName;
    [SerializeField] private string fogPlantName;
    [SerializeField] private Interaction thisInteraction;
    private bool lightBulbIn;
    private bool fogPlantIn;

    private void Start()
    {
        objectToOperate.SetActive(baseActiveState);
    }

    public void OnSignalReceived(GameObject source)
    {
        CheckPlantStatus();
    }

    private void CheckPlantStatus()
    {
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(lightBulbName)) lightBulbIn = true;
        else lightBulbIn = false;
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(fogPlantName)) fogPlantIn = true;
        else fogPlantIn = false;
        SetInvisibilityStatus();
    }

    private void SetInvisibilityStatus()
    {
        if (lightBulbIn && !fogPlantIn) objectToOperate.SetActive(true);
        else if (!lightBulbIn && fogPlantIn) objectToOperate.SetActive(false);
        else objectToOperate.SetActive(baseActiveState);
    }
}

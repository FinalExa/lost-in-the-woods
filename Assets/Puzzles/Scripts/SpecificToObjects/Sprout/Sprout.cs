using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour, ISendSignalToSelf
{
    public SproutRoot[] sproutRoots;
    [SerializeField] private string lampPlantName;
    [SerializeField] private string corruptionPlantName;
    [SerializeField] private string guidingLightPlantName;
    [SerializeField] private string darkMistPlantName;
    [SerializeField] private string corruptionAuraSproutName;
    [SerializeField] private DeadSprout deadSproutRef;
    private Interaction thisInteraction;
    private bool lampPlantIn;
    private bool corruptionPlantIn;
    private bool guidingLightPlantIn;
    private bool darkMistPlantIn;

    private void Awake()
    {
        thisInteraction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        ActivateSproutRoots();
    }

    public void OnSignalReceived(GameObject source)
    {
        if (thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(corruptionAuraSproutName)) CreateDeadSprout();
        else CheckPlantStatus();
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

    private void ActivateSproutRoots()
    {
        foreach (SproutRoot root in sproutRoots) root.gameObject.SetActive(true);
    }

    private void CreateDeadSprout()
    {
        DeadSprout deadSprout = Instantiate(deadSproutRef, this.transform.position, Quaternion.identity);
        deadSprout.sproutRoots = sproutRoots;
        GameObject.Destroy(this.gameObject);
    }
}

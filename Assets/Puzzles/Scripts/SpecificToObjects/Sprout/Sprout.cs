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
    [SerializeField] private bool lampPlantIn;
    [SerializeField] private bool corruptionPlantIn;
    [SerializeField] private bool guidingLightPlantIn;
    [SerializeField] private bool darkMistPlantIn;

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
        if (NameStatus(corruptionAuraSproutName)) CreateDeadSprout();
        else CheckPlantStatus();
    }

    private void CheckPlantStatus()
    {
        lampPlantIn = NameStatus(lampPlantName);
        corruptionPlantIn = NameStatus(corruptionPlantName);
        guidingLightPlantIn = NameStatus(guidingLightPlantName);
        darkMistPlantIn = NameStatus(darkMistPlantName);
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

    private bool NameStatus(string nameToCheck)
    {
        return thisInteraction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(nameToCheck);
    }
}

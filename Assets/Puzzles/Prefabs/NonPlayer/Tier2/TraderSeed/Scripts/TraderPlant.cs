using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderPlant : MonoBehaviour, ISendSignalToSelf
{
    private Interaction interaction;
    [SerializeField] private string signalNamePositive;
    [SerializeField] private string signalNameNegative;
    [SerializeField] private string explorationNamePositive;
    [SerializeField] private string explorationNameNegative;
    [HideInInspector] public int signalSetState;
    [HideInInspector] public int explorationSetState;

    private void Awake()
    {
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        SetTraderPlantStatus(interaction.namedInteractionOperations);
    }

    public void OnSignalReceived(GameObject source)
    {
        SetTraderPlantStatus(interaction.namedInteractionOperations);
    }

    private void SetTraderPlantStatus(NamedInteractionOperations namedOps)
    {
        SetSignals(namedOps);
        SetExploration(namedOps);
    }

    private void SetSignals(NamedInteractionOperations namedOps)
    {
        signalSetState = 0;
        if (namedOps.ActiveNamedInteractions.ContainsKey(signalNamePositive)) signalSetState++;
        if (namedOps.ActiveNamedInteractions.ContainsKey(signalNameNegative)) signalSetState--;
    }

    private void SetExploration(NamedInteractionOperations namedOps)
    {
        explorationSetState = 0;
        if (namedOps.ActiveNamedInteractions.ContainsKey(explorationNamePositive)) explorationSetState++;
        if (namedOps.ActiveNamedInteractions.ContainsKey(explorationNameNegative)) explorationSetState--;
    }

}

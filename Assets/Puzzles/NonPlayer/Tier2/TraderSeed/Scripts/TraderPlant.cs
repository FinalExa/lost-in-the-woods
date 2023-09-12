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
    [SerializeField] private SpriteRenderer signalFeedback;
    [SerializeField] private Color signalPositiveColor;
    [SerializeField] private Color signalNegativeColor;
    [SerializeField] private SpriteRenderer explorationFeedback;
    [SerializeField] private Color explorationPositiveColor;
    [SerializeField] private Color explorationNegativeColor;

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
        signalSetState = 1;
        if (namedOps.ActiveNamedInteractions.ContainsKey(signalNamePositive)) signalSetState++;
        if (namedOps.ActiveNamedInteractions.ContainsKey(signalNameNegative)) signalSetState--;
        if (signalSetState == 1) signalFeedback.gameObject.SetActive(false);
        else
        {
            signalFeedback.gameObject.SetActive(true);
            if (signalSetState == 2) signalFeedback.color = signalPositiveColor;
            else if (signalSetState == 0) signalFeedback.color = signalNegativeColor;
        }
    }

    private void SetExploration(NamedInteractionOperations namedOps)
    {
        explorationSetState = 1;
        if (namedOps.ActiveNamedInteractions.ContainsKey(explorationNamePositive)) explorationSetState++;
        if (namedOps.ActiveNamedInteractions.ContainsKey(explorationNameNegative)) explorationSetState--;
        if (explorationSetState == 1) explorationFeedback.gameObject.SetActive(false);
        else
        {
            explorationFeedback.gameObject.SetActive(true);
            if (explorationSetState == 2) explorationFeedback.color = explorationPositiveColor;
            else if (explorationSetState == 0) explorationFeedback.color = explorationNegativeColor;
        }
    }

}

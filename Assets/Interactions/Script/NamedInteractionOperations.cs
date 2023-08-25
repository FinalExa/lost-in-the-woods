using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedInteractionOperations
{
    private Interaction interactionRef;
    public Dictionary<string, int> ActiveNamedInteractions { get; private set; }
    public NamedInteractionOperations(Interaction reference)
    {
        interactionRef = reference;
        ActiveNamedInteractions = new Dictionary<string, int>();
    }

    public void NamedInteractionEnter(NamedInteractionExecutor interactionExecutor, GameObject sourceObject)
    {
        foreach (SetOfInteractions.NamedInteraction interaction in interactionRef.setOfInteractions.namedInteractions)
        {
            if (interaction.name == interactionExecutor.thisName)
            {
                if (!ActiveNamedInteractions.ContainsKey(interactionExecutor.thisName))
                {
                    if (interaction.destroyNamedObjectOnInteraction)
                    {
                        ActiveNamedInteractions.Add(interactionExecutor.thisName, 1);
                        interactionRef.interactionOptions.Interact(interaction.options, sourceObject, interactionRef.setOfInteractions.turnsOff);
                        RemoveOrReduceNamedInteractionFromList(interaction, interactionExecutor, sourceObject);
                        DestroyObjectAfterInteraction(interaction.turnOffNamedObjectInsteadOfDestroy, interactionExecutor.gameObject);
                    }
                    else
                    {
                        ActiveNamedInteractions.Add(interactionExecutor.thisName, 1);
                        interactionRef.interactionOptions.Interact(interaction.options, sourceObject, interactionRef.setOfInteractions.turnsOff);
                    }
                }
                else ActiveNamedInteractions[interactionExecutor.thisName]++;
                break;
            }
        }
    }

    private void DestroyObjectAfterInteraction(bool turnOff, GameObject objectToDestroy)
    {
        Health health = objectToDestroy.GetComponent<Health>();
        if (health != null) health.OnDeath(true);
        else
        {
            NamedInteractionExecutor namedInteractionExecutor = objectToDestroy.GetComponent<NamedInteractionExecutor>();
            if (turnOff) namedInteractionExecutor.refObject.SetActive(false);
            else GameObject.Destroy(namedInteractionExecutor.refObject);
        }
    }

    public void NamedInteractionExit(NamedInteractionExecutor interactionExecutor, GameObject sourceObject)
    {
        foreach (SetOfInteractions.NamedInteraction interaction in interactionRef.setOfInteractions.namedInteractions)
        {
            if (interaction.name == interactionExecutor.thisName && ActiveNamedInteractions.ContainsKey(interactionExecutor.thisName))
            {
                RemoveOrReduceNamedInteractionFromList(interaction, interactionExecutor, sourceObject);
            }
        }
    }

    private void RemoveOrReduceNamedInteractionFromList(SetOfInteractions.NamedInteraction interaction, NamedInteractionExecutor interactionExecutor, GameObject sourceObject)
    {
        if (ActiveNamedInteractions[interactionExecutor.thisName] > 1) ActiveNamedInteractions[interactionExecutor.thisName]--;
        else
        {
            ActiveNamedInteractions.Remove(interactionExecutor.thisName);
            if (interaction.hasNamedInteractionExitOptions) interactionRef.interactionOptions.Interact(interaction.exitNamedInteractionOptions, sourceObject, interactionRef.setOfInteractions.turnsOff);
        }
    }
}

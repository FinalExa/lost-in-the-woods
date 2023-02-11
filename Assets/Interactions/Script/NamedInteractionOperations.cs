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
                    interactionRef.interactionOptions.Interact(interaction.options, sourceObject, interactionRef.setOfInteractions.turnsOff);
                    if (interaction.destroyNamedObjectOnInteraction) interactionExecutor.DestroyOnDone();
                    else ActiveNamedInteractions.Add(interactionExecutor.thisName, 1);
                }
                else ActiveNamedInteractions[interactionExecutor.thisName]++;
                break;
            }
        }
    }

    public void NamedInteractionExit(NamedInteractionExecutor interactionExecutor, GameObject sourceObject)
    {
        foreach (SetOfInteractions.NamedInteraction interaction in interactionRef.setOfInteractions.namedInteractions)
        {
            if (interaction.name == interactionExecutor.thisName && ActiveNamedInteractions.ContainsKey(interactionExecutor.thisName))
            {
                if (ActiveNamedInteractions[interactionExecutor.thisName] > 1) ActiveNamedInteractions[interactionExecutor.thisName]--;
                else
                {
                    if (interaction.hasNamedInteractionExitOptions) interactionRef.interactionOptions.Interact(interaction.exitNamedInteractionOptions, sourceObject, interactionRef.setOfInteractions.turnsOff);
                    ActiveNamedInteractions.Remove(interactionExecutor.thisName);
                }
            }
        }
    }
}

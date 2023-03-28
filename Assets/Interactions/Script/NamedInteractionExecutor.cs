using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedInteractionExecutor : MonoBehaviour
{
    public string thisName;
    public bool active;
    private List<Interaction> interactingWith;
    [SerializeField] private bool inLoop;

    private void Awake()
    {
        interactingWith = new List<Interaction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!inLoop) ExecuteNamedInteraction(other.GetComponent<Interaction>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (inLoop) ExecuteNamedInteraction(other.GetComponent<Interaction>());
    }

    private void OnTriggerExit(Collider other)
    {
        ExecuteExitFromNamedInteraction(other.GetComponent<Interaction>());
    }

    private void ExecuteNamedInteraction(Interaction interaction)
    {
        if (interaction != null && active && !interactingWith.Contains(interaction))
        {
            interaction.NamedInteractionExecute(this, this.gameObject);
            interactingWith.Add(interaction);
        }
    }
    private void ExecuteExitFromNamedInteraction(Interaction interaction)
    {
        if (interaction != null && active && interactingWith.Contains(interaction)) interaction.ExitFromNamedInteraction(this, this.gameObject);
    }

    private void ExitFromAllInteractions()
    {
        if (interactingWith != null)
        {
            foreach (Interaction interaction in interactingWith)
            {
                if (this.gameObject != null && interaction != null) interaction.ExitFromNamedInteraction(this, this.gameObject);
            }
            interactingWith.Clear();
        }
    }

    private void OnDisable()
    {
        ExitFromAllInteractions();
    }
}

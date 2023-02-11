using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedInteractionExecutor : MonoBehaviour
{
    public string thisName;
    public bool active;
    [SerializeField] private bool inLoop;
    [HideInInspector] public bool interactionDone { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!inLoop) ExecuteNamedInteraction(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (inLoop) ExecuteNamedInteraction(other);
    }

    private void OnTriggerExit(Collider other)
    {
        ExecuteExitFromNamedInteraction(other);
    }

    private void ExecuteNamedInteraction(Collider other)
    {
        Interaction interaction = other.GetComponent<Interaction>();
        if (interaction != null && active)
        {
            interaction.NamedInteractionExecute(this, this.gameObject);
            interactionDone = true;
        }
    }
    private void ExecuteExitFromNamedInteraction(Collider other)
    {
        Interaction interaction = other.GetComponent<Interaction>();
        if (interaction != null && active) interaction.ExitFromNamedInteraction(this, this.gameObject);
    }

    public void DestroyOnDone()
    {
        GameObject.Destroy(this.gameObject);
    }

}

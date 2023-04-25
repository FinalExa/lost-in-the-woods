using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedInteractionExecutor : MonoBehaviour
{
    public string thisName;
    public bool active;
    private List<Interaction> interactingWith;
    [SerializeField] private bool inLoop;
    private BoxCollider thisBoxCollider;
    private SphereCollider thisSphereCollider;

    private void Awake()
    {
        interactingWith = new List<Interaction>();
        thisBoxCollider = this.gameObject.GetComponent<BoxCollider>();
        thisSphereCollider = this.gameObject.GetComponent<SphereCollider>();
    }

    private void Start()
    {
        ForceCheck();
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

    private void ForceCheck()
    {
        if (thisBoxCollider != null) ForceLaunchInteractions(Physics.OverlapBox(this.transform.position, thisBoxCollider.size / 2));
        else if (thisSphereCollider != null) ForceLaunchInteractions(Physics.OverlapSphere(this.transform.position, thisSphereCollider.radius));
    }

    private void ForceLaunchInteractions(Collider[] arrayOfColliders)
    {
        foreach (Collider collider in arrayOfColliders)
        {
            ExecuteNamedInteraction(collider.gameObject.GetComponent<Interaction>());
        }
    }

    public void NameAndStateChange(string newName, bool newState)
    {
        ExitFromAllInteractions();
        thisName = newName;
        active = newState;
        ForceCheck();
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
        if (interaction != null && active && interactingWith.Contains(interaction))
        {
            interaction.ExitFromNamedInteraction(this, this.gameObject);
            interactingWith.Remove(interaction);
        }
    }

    private void ExitFromAllInteractions()
    {
        if (interactingWith != null && interactingWith.Count > 0 && active)
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

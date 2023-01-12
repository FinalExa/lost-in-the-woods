using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNamedInteractionExecutor : MonoBehaviour
{
    [SerializeField] private string thisName;
    [HideInInspector] public bool active;
    [SerializeField] private bool inLoop;
    [HideInInspector] public bool interactionDone;

    private void OnTriggerEnter(Collider other)
    {
        if (!inLoop) ExecuteGenericInteraction(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (inLoop) ExecuteGenericInteraction(other);
    }

    private void ExecuteGenericInteraction(Collider other)
    {
        AttackInteraction attackInteraction = other.GetComponent<AttackInteraction>();
        if (attackInteraction != null && active)
        {
            attackInteraction.NamedInteractionExecute(thisName, this.gameObject);
            interactionDone = true;
        }
    }
}

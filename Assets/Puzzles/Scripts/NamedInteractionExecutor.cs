using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamedInteractionExecutor : MonoBehaviour
{
    [SerializeField] private string thisName;
    public bool active;
    [SerializeField] private bool inLoop;
    [HideInInspector] public bool interactionDone;

    private void OnTriggerEnter(Collider other)
    {
        if (!inLoop) ExecuteNamedInteraction(other);
    }

    private void OnTriggerStay(Collider other)
    {
        if (inLoop) ExecuteNamedInteraction(other);
    }

    private void ExecuteNamedInteraction(Collider other)
    {
        AttackInteraction attackInteraction = other.GetComponent<AttackInteraction>();
        if (attackInteraction != null && active)
        {
            attackInteraction.NamedInteractionExecute(thisName, this.gameObject, this);
            interactionDone = true;
            DestroyOnDone();
        }
    }

    public void DestroyOnDone()
    {
        GameObject.Destroy(this.gameObject);
    }

}

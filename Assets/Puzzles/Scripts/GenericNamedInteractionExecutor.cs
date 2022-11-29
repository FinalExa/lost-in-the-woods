using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNamedInteractionExecutor : MonoBehaviour
{
    [SerializeField] private string thisName;
    [SerializeField] private string effectiveTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(effectiveTag))
        {
            other.GetComponent<AttackInteraction>().NamedInteractionExecute(thisName);
        }
    }
}

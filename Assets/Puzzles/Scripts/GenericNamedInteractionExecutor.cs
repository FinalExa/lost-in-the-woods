using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNamedInteractionExecutor : MonoBehaviour
{
    [SerializeField] private string thisName;
    [SerializeField] private bool disableOnEnd;
    private string receivedString;

    private void OnTriggerEnter(Collider other)
    {
        AttackInteraction attackInteraction = other.GetComponent<AttackInteraction>();
        if (attackInteraction != null)
        {
            receivedString = attackInteraction.NamedInteractionExecute(thisName);
            if (receivedString == thisName && disableOnEnd) this.gameObject.SetActive(false);
        }
    }
}

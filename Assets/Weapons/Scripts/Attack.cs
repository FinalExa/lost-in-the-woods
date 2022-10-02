using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [HideInInspector] public string whoToDamage;
    [HideInInspector] public bool canApplyElement;

    protected Collider otherCollider;
    protected Health otherHealth;

    private void OnTriggerEnter(Collider other)
    {
        GetOtherReferences(other);
        Damage();
    }

    protected virtual void GetOtherReferences(Collider other)
    {
        otherCollider = other;
        otherHealth = other.gameObject.GetComponent<Health>();
    }
    protected virtual void Damage()
    {
        return;
    }
}

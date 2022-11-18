using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReceived : MonoBehaviour
{
    private Health health;
    [SerializeField] private AttackReceivedData attackReceivedData;

    private void Awake()
    {
        health = this.gameObject.GetComponent<Health>();
    }

    public void AttackReceivedOperation(float damage)
    {
        if (!attackReceivedData.ignoresDamage) health.HealthAddValue(-damage);
    }
}

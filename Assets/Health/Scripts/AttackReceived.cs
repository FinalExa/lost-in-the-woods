using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackReceived : MonoBehaviour
{
    private Health health;
    private AttackInteraction attackInteraction;
    [SerializeField] private AttackReceivedData attackReceivedData;

    private void Awake()
    {
        health = this.gameObject.GetComponent<Health>();
        attackInteraction = this.gameObject.GetComponent<AttackInteraction>();
    }

    public void AttackReceivedOperation(List<AttackReceivedData.GameTargets> receivedTargets, float damage, WeaponAttack.WeaponAttackType weaponAttackType)
    {
        print(weaponAttackType);
        if (receivedTargets.Contains(attackReceivedData.targetType))
        {
            if (!attackReceivedData.ignoresDamage && health != null) health.HealthAddValue(-damage);
        }
    }
}

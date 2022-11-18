using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    protected override void Damage()
    {
        if (attackReceived.gameObject.CompareTag(damageTag) && !thisWeapon.hitTargets.Contains(attackReceived))
        {
            attackReceived.AttackReceivedOperation(thisWeapon.currentDamage);
            thisWeapon.hitTargets.Add(attackReceived);
        }
    }
}

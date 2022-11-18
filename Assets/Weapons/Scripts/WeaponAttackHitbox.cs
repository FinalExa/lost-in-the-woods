using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    protected override void Damage()
    {
        if (!thisWeapon.hitTargets.Contains(attackReceived))
        {
            attackReceived.AttackReceivedOperation(possibleTargets, thisWeapon.currentDamage);
            thisWeapon.hitTargets.Add(attackReceived);
        }
    }
}

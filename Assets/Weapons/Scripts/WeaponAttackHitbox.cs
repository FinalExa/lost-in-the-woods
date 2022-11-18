using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    [HideInInspector] public WeaponAttack.WeaponAttackType weaponAttackType;
    protected override void Damage()
    {
        if (!thisWeapon.hitTargets.Contains(attackReceived))
        {
            attackReceived.AttackReceivedOperation(possibleTargets, thisWeapon.currentDamage);
            thisWeapon.hitTargets.Add(attackReceived);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    [HideInInspector] public List<WeaponAttack.WeaponAttackType> weaponAttackTypes;
    protected override void Damage()
    {
        if (!thisWeapon.hitTargets.Contains(attackReceived))
        {
            attackReceived.AttackReceivedOperation(possibleTargets, thisWeapon.currentDamage, weaponAttackTypes);
            thisWeapon.hitTargets.Add(attackReceived);
        }
    }
}

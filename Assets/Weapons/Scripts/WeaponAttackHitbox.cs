using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    [HideInInspector] public List<WeaponAttack.WeaponAttackType> weaponAttackTypes;
    protected override void Damage(string receivedTag)
    {
        bool invulnerable = false;
        if (receivedTag == "Invulnerable") invulnerable = true;
        if (!thisWeapon.hitTargets.Contains(attackReceived))
        {
            attackReceived.AttackReceivedOperation(possibleTargets, thisWeapon.currentDamage, weaponAttackTypes, invulnerable, thisWeapon.gameObject);
            thisWeapon.hitTargets.Add(attackReceived);
        }
    }
}

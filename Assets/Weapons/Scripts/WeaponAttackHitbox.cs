using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    protected override void Damage()
    {
        if (otherHealth != null && (otherCollider.CompareTag(whoToDamage) || otherCollider.CompareTag("Invulnerable")) && !thisWeapon.hitTargets.Contains(otherHealth))
        {
            if (otherCollider.CompareTag(whoToDamage)) otherHealth.HealthAddValue(-thisWeapon.currentDamage);
            thisWeapon.hitTargets.Add(otherHealth);
        }
    }
}

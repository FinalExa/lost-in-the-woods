using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackHitbox : Attack
{
    [HideInInspector] public Weapon thisWeapon;
    [HideInInspector] public List<WeaponAttack.WeaponAttackType> weaponAttackTypes;
    [SerializeField] private LayerMask hitLayer;
    protected override void Damage(string receivedTag)
    {
        bool invulnerable = false;
        if (receivedTag == "Invulnerable") invulnerable = true;
        if (thisWeapon!=null) DamagePossible(invulnerable);
    }

    private void DamagePossible(bool invulnerable)
    {
        if (thisWeapon.weaponAttacks[thisWeapon.currentWeaponAttackIndex].ignoresWalls) AttackIsReceived(invulnerable);
        else WallCheck(invulnerable);
    }

    private void AttackIsReceived(bool invulnerable)
    {
        if (!thisWeapon.hitTargets.Contains(attackReceived)) SetHit(invulnerable);
    }

    private void WallCheck(bool invulnerable)
    {
        Vector3 origin = thisWeapon.gameObject.transform.position;
        Vector3 direction = attackReceived.gameObject.transform.position - origin;
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, Mathf.Infinity, hitLayer))
        {
            if (hit.collider.gameObject == attackReceived.gameObject) AttackIsReceived(invulnerable);
        }
    }

    private void SetHit(bool invulnerable)
    {
        attackReceived.AttackReceivedOperation(possibleTargets, thisWeapon.currentDamage, weaponAttackTypes, invulnerable, thisWeapon.gameObject);
        thisWeapon.hitTargets.Add(attackReceived);
    }
}

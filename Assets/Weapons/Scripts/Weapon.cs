using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float comboCancelTime;
    public float comboEndDelay;
    public WeaponAttack[] weaponAttacks;
    public List<Health> hitTargets;
    [HideInInspector] public string damageTag;
    [HideInInspector] public float currentDamage;

    private void Start()
    {
        ReferencesSetup();
    }

    private void ReferencesSetup()
    {
        foreach (WeaponAttack weaponAttack in weaponAttacks)
        {
            for (int i = 0; i < weaponAttack.weaponAttackHitboxSequence.Length; i++)
            {
                WeaponAttackHitbox attackToSet = weaponAttack.weaponAttackHitboxSequence[i].attackRef.gameObject.GetComponent<WeaponAttackHitbox>();
                weaponAttack.weaponAttackHitboxSequence[i].attackRef = attackToSet;
                if (attackToSet != null)
                {
                    attackToSet.thisWeapon = this;
                    attackToSet.damageTag = damageTag;
                }
            }
        }
    }
}

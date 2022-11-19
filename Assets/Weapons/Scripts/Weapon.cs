using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public float comboCancelTime;
    public float comboEndDelay;
    public List<AttackReceivedData.GameTargets> possibleTargets;
    public List<WeaponAttack> weaponAttacks;
    [HideInInspector] public List<AttackReceived> hitTargets;
    [HideInInspector] public float currentDamage;


    private void Start()
    {
        ReferencesSetup();
    }

    private void ReferencesSetup()
    {
        hitTargets = new List<AttackReceived>();
        foreach (WeaponAttack weaponAttack in weaponAttacks)
        {
            for (int i = 0; i < weaponAttack.weaponAttackHitboxSequence.Length; i++)
            {
                WeaponAttackHitbox attackToSet = weaponAttack.weaponAttackHitboxSequence[i].attackRef.gameObject.GetComponent<WeaponAttackHitbox>();
                Projectile projectile = weaponAttack.weaponAttackHitboxSequence[i].projectile;
                if (projectile != null)
                {
                    if (weaponAttack.weaponAttackHitboxSequence[i].spawnsProjectile) projectile.possibleTargets = possibleTargets;
                    projectile.gameObject.SetActive(false);
                }
                weaponAttack.weaponAttackHitboxSequence[i].attackRef = attackToSet;
                if (attackToSet != null)
                {
                    attackToSet.thisWeapon = this;
                    attackToSet.possibleTargets = possibleTargets;
                    attackToSet.weaponAttackType = weaponAttack.weaponAttackType;
                }
            }
        }
    }

    #region Auto Initialization
    [ContextMenu("Initialize weapon attacks (destroys current list)")]
    private void AutoBuild()
    {
        weaponAttacks.Clear();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            WeaponAttack attack = new WeaponAttack();
            attack.attackObject = this.transform.GetChild(i).gameObject;
            AutoSetAttackHitboxes(attack, attack.attackObject);
            weaponAttacks.Add(attack);
        }
    }
    private void AutoSetAttackHitboxes(WeaponAttack attack, GameObject attackObject)
    {
        List<WeaponAttack.WeaponAttackHitboxSequence> sequence = new List<WeaponAttack.WeaponAttackHitboxSequence>();
        for (int i = 0; i < attackObject.transform.childCount; i++)
        {
            WeaponAttackHitbox attackHitbox = attackObject.transform.GetChild(i).gameObject.GetComponent<WeaponAttackHitbox>();
            if (attackHitbox != null)
            {
                WeaponAttack.WeaponAttackHitboxSequence sequenceElement = new WeaponAttack.WeaponAttackHitboxSequence();
                sequenceElement.attackRef = attackHitbox;
                if (attackHitbox.gameObject.transform.childCount > 0)
                {
                    Projectile projectile = attackHitbox.gameObject.GetComponentInChildren<Projectile>();
                    if (projectile != null)
                    {
                        sequenceElement.spawnsProjectile = true;
                        sequenceElement.projectile = projectile;
                    }
                }
                sequence.Add(sequenceElement);
            }
        }
        if (sequence.Count > 0) attack.weaponAttackHitboxSequence = sequence.ToArray();
    }
    #endregion
}

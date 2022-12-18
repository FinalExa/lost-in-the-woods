using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public int comboEndFramesDelay;
    public bool comboCancelEnabled;
    public int comboCancelFrames;
    public List<AttackReceived.GameTargets> possibleTargets;
    public List<WeaponAttack> weaponAttacks;
    [HideInInspector] public List<AttackReceived> hitTargets;
    [HideInInspector] public float currentDamage;
    [HideInInspector] public int currentWeaponAttackIndex;


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
                weaponAttack.uxOnWeaponAttack.UXEffectStartup();
                WeaponAttackHitbox attackToSet = weaponAttack.weaponAttackHitboxSequence[i].attackRef.gameObject.GetComponent<WeaponAttackHitbox>();
                weaponAttack.weaponAttackHitboxSequence[i].attackRef = attackToSet;
                if (attackToSet != null)
                {
                    attackToSet.thisWeapon = this;
                    attackToSet.possibleTargets = possibleTargets;
                    attackToSet.weaponAttackTypes = weaponAttack.weaponAttackTypes;
                    if (attackToSet.gameObject.activeSelf) attackToSet.gameObject.SetActive(false);
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
            attack.weaponAttackTypes = new List<WeaponAttack.WeaponAttackType>();
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
                sequence.Add(sequenceElement);
            }
        }
        if (sequence.Count > 0) attack.weaponAttackHitboxSequence = sequence.ToArray();
    }
    #endregion
}

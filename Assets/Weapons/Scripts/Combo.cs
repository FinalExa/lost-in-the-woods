using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public WeaponAttack currentAttack;
    [HideInInspector] public bool comboHitOver;
    [HideInInspector] public bool comboDelay;
    [HideInInspector] public bool comboEndDelay;
    [HideInInspector] public float comboDelayTimer;
    [HideInInspector] public float comboCancelTimer;
    [HideInInspector] public Vector3 lastDirection { get; set; }
    protected bool comboCancelDelay;
    protected float movementSpeed;
    [HideInInspector] public ComboObjectSpawner comboObjectSpawner;
    protected ComboAttack comboAttack;

    protected virtual void Awake()
    {
        comboObjectSpawner = new ComboObjectSpawner();
        comboAttack = new ComboAttack(this);
    }

    protected virtual void Start()
    {
        ComboSetup();
    }
    public virtual void Update()
    {
        comboAttack.Attacking();
        if (comboDelay) ComboDelay();
        if (comboCancelDelay && currentWeapon.comboCancelEnabled) CountToCancelCombo();
    }

    public void SetWeapon(Weapon weaponToSet)
    {
        if (currentWeapon != weaponToSet)
        {
            currentWeapon = weaponToSet;
            currentWeapon.currentWeaponAttackIndex = 0;
        }
    }
    protected void ComboSetup()
    {
        comboHitOver = true;
        comboDelay = false;
        lastDirection = new Vector3(0f, 0f, 1f);
    }
    public void StartComboHitCheck()
    {
        if (!comboDelay) StartComboHit();
    }

    protected void ComboDelay()
    {
        if (comboDelayTimer > 0) comboDelayTimer -= Time.deltaTime;
        else
        {
            comboDelay = false;
            if (comboEndDelay)
            {
                comboEndDelay = false;
                OnComboEnd();
            }
        }
    }

    protected void StartComboHit()
    {
        comboHitOver = false;
        comboCancelDelay = false;
        currentWeapon.currentDamage = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].damage;
        currentAttack = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex];
        comboAttack.StartAttack(currentWeapon, currentAttack, lastDirection);
    }

    private void CountToCancelCombo()
    {
        if (comboCancelTimer > 0) comboCancelTimer -= Time.deltaTime;
        else
        {
            currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(false);
            currentWeapon.currentWeaponAttackIndex = 0;
            currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(true);
            comboCancelDelay = false;
        }
    }

    public void EndComboHit()
    {
        currentWeapon.hitTargets.Clear();
        if (currentWeapon.currentWeaponAttackIndex + 1 == currentWeapon.weaponAttacks.Count)
        {
            currentWeapon.currentWeaponAttackIndex = 0;
            comboDelayTimer = currentWeapon.comboEndDelay;
            comboEndDelay = true;
        }
        else
        {
            comboDelayTimer = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].afterDelay;
            currentWeapon.currentWeaponAttackIndex++;
            comboCancelTimer = currentWeapon.comboCancelTime;
            comboCancelDelay = true;
        }
        comboDelay = true;
        comboHitOver = true;
    }
    protected virtual void OnComboEnd()
    {
        return;
    }

    public bool GetIfIsAttacking()
    {
        return comboAttack.isAttacking;
    }

    public void EndCombo()
    {
        comboAttack.EndCombo();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public WeaponAttack currentAttack;
    [HideInInspector] public Vector3 LastDirection { get; set; }
    [HideInInspector] public ComboObjectSpawner comboObjectSpawner;
    protected ComboAttack comboAttack;
    protected ComboDelays comboDelays;

    protected virtual void Awake()
    {
        comboObjectSpawner = new ComboObjectSpawner();
        comboAttack = new ComboAttack(this);
        comboDelays = new ComboDelays(this);
    }

    protected virtual void Start()
    {
        ComboSetup();
    }
    public virtual void Update()
    {
        comboAttack.Attacking();
        comboDelays.ComboDelay();
        comboDelays.CancelComboTimer();
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
        comboDelays.SetVariablesReadyForAttack();
        LastDirection = new Vector3(0f, 0f, 1f);
    }
    public bool StartComboHitCheck()
    {
        if (comboDelays.CheckIfHitIsPossible())
        {
            StartComboHit();
            return true;
        }
        else return false;
    }

    protected void StartComboHit()
    {
        comboDelays.SetVariablesDuringAttack();
        currentWeapon.currentDamage = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].damage;
        currentAttack = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex];
        comboAttack.StartAttack(currentWeapon, currentAttack, LastDirection);
    }



    public void EndComboHit()
    {
        currentWeapon.hitTargets.Clear();
        if (currentWeapon.currentWeaponAttackIndex + 1 == currentWeapon.weaponAttacks.Count)
        {
            currentWeapon.currentWeaponAttackIndex = 0;
            comboDelays.SetComboEnd();
        }
        else
        {
            currentWeapon.currentWeaponAttackIndex++;
            comboDelays.SetDelayForNextAttack();
        }
        comboDelays.SetVariablesAfterAttack();
    }
    public virtual void OnComboEnd()
    {
        return;
    }

    public bool GetIfIsAttacking()
    {
        return comboAttack.IsAttacking;
    }

    public bool GetHitOver()
    {
        return comboDelays.CheckIfHitIsOver();
    }

    public void EndCombo()
    {
        comboAttack.EndCombo();
    }
}

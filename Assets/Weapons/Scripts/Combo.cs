using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combo : MonoBehaviour
{
    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public WeaponAttack currentAttack;
    [HideInInspector] public Vector3 LastDirection { get; set; }
    [HideInInspector] public ComboObjectSpawner comboObjectSpawner;
    [HideInInspector] public ComboDelays comboDelays;
    [HideInInspector] public ComboAttack comboAttack;

    protected virtual void Awake()
    {
        ComboFrameMaster.frameIsBeingExecuted += ExecuteComboOperations;
        comboObjectSpawner = new ComboObjectSpawner(this);
        comboDelays = new ComboDelays(this);
        comboAttack = new ComboAttack(this);
    }

    protected virtual void Start()
    {
        ComboSetup();
    }

    private void ExecuteComboOperations()
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
    public void StartComboHitCheck()
    {
        if (GetIfHitIsPossible()) StartComboHit();
    }

    protected void StartComboHit()
    {
        comboDelays.SetVariablesDuringAttack();
        currentWeapon.currentDamage = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex].damage;
        currentAttack = currentWeapon.weaponAttacks[currentWeapon.currentWeaponAttackIndex];
        comboAttack.StartAttack(currentAttack, LastDirection);
    }
    public virtual void OnComboEnd()
    {
        return;
    }

    public bool GetIfHitIsPossible()
    {
        return comboDelays.CheckIfHitIsPossible();
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

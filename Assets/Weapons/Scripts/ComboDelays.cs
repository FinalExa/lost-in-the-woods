using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDelays
{
    private Combo combo;
    private bool comboHitOver;
    private bool comboDelay;
    private bool comboEndDelay;
    private float comboDelayTimer;
    private bool comboCancelActive;
    private float comboCancelTimer;

    public ComboDelays(Combo comboRef)
    {
        combo = comboRef;
    }

    public void SetVariablesReadyForAttack()
    {
        comboHitOver = true;
        comboDelay = false;
    }

    public void SetVariablesDuringAttack()
    {
        comboHitOver = false;
        comboDelay = false;
    }

    public void SetVariablesAfterAttack()
    {
        comboHitOver = true;
        comboDelay = true;
    }

    public bool CheckIfHitIsPossible()
    {
        if (CheckIfHitIsOver() && !comboDelay) return true;
        else return false;
    }

    public bool CheckIfHitIsOver()
    {
        return comboHitOver;
    }

    public bool CheckIfInEndCombo()
    {
        return comboEndDelay;
    }

    public void ComboDelay()
    {
        if (comboDelay)
        {
            if (comboDelayTimer > 0) comboDelayTimer -= Time.deltaTime;
            else
            {
                comboDelay = false;
                if (comboEndDelay)
                {
                    comboEndDelay = false;
                    combo.OnComboEnd();
                }
            }
        }
    }
    public void CancelComboTimer()
    {
        if (comboCancelActive && combo.currentWeapon.comboCancelEnabled)
        {
            if (comboCancelTimer > 0) comboCancelTimer -= Time.deltaTime;
            else OnComboCancel();
        }
    }

    private void OnComboCancel()
    {
        combo.currentWeapon.weaponAttacks[combo.currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(false);
        combo.currentWeapon.currentWeaponAttackIndex = 0;
        combo.currentWeapon.weaponAttacks[combo.currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(true);
        comboCancelActive = false;
    }

    public void SetComboEnd()
    {
        comboDelayTimer = combo.currentWeapon.comboEndDelay;
        comboEndDelay = true;
        comboCancelActive = false;
    }

    public void SetDelayForNextAttack()
    {
        comboDelayTimer = combo.currentAttack.afterDelay;
        comboCancelTimer = combo.currentWeapon.comboCancelTime;
        comboCancelActive = true;
    }
}
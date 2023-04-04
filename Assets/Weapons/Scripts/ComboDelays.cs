using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDelays
{
    private Combo combo;
    private bool comboHitOver;
    private bool comboDelay;
    private bool comboEndDelay;
    private int comboDelayCountdownFrames;
    private bool comboCancelActive;
    private float comboDelayCancelFrames;

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
        comboCancelActive = false;
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
            if (comboDelayCountdownFrames > 0) comboDelayCountdownFrames--;
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
            if (comboDelayCancelFrames > 0) comboDelayCancelFrames--;
            else ComboCanceled();
        }
    }

    private void ComboCanceled()
    {
        if (combo.currentWeapon.weaponAttacks[combo.currentWeapon.currentWeaponAttackIndex].attackObject != null)
        {
            combo.currentWeapon.weaponAttacks[combo.currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(false);
            combo.currentWeapon.currentWeaponAttackIndex = 0;
            combo.currentWeapon.weaponAttacks[combo.currentWeapon.currentWeaponAttackIndex].attackObject.SetActive(true);
            comboCancelActive = false;
        }
    }

    public void SetComboEnd()
    {
        comboDelayCountdownFrames = combo.currentWeapon.comboEndFramesDelay;
        comboEndDelay = true;
        comboCancelActive = false;
    }

    public void SetDelayForNextAttack()
    {
        comboDelayCountdownFrames = combo.currentAttack.framesOfDelay;
        SetComboCancel();
    }

    private void SetComboCancel()
    {
        comboCancelActive = true;
        comboDelayCancelFrames = combo.currentWeapon.comboCancelFrames;
    }
}
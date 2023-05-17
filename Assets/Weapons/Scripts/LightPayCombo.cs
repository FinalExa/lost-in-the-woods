using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPayCombo : Combo
{
    [SerializeField] private Weapon weaponToSet;
    private Weapon weaponInQueue;

    protected override void Start()
    {
        SetWeapon(weaponToSet);
        base.Start();
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (!comboActive)
        {
            currentWeapon = newWeapon;
            weaponInQueue = null;
        }
        else weaponInQueue = newWeapon;
    }

    public void ExecuteLightPayCombo()
    {
        if (currentWeapon != null) StartComboHitCheck();
    }

    public void StopLightPayCombo()
    {
        if (comboActive) EndCombo();
    }

    public override void EndCombo()
    {
        base.EndCombo();
        CheckForQueue();
    }

    private void CheckForQueue()
    {
        if (weaponInQueue != null && weaponInQueue != currentWeapon)
        {
            SetWeapon(weaponInQueue);
            weaponInQueue = null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : Combo
{
    [SerializeField] private Weapon playerMainWeapon;
    [SerializeField] private Weapon playerSecondaryWeapon;

    public void StartHitOnWeapon(bool isSecondary)
    {
        Weapon weaponToSet;
        if (!isSecondary) weaponToSet = playerMainWeapon;
        else weaponToSet = playerSecondaryWeapon;
        if (weaponToSet != currentWeapon) DifferentWeapons(weaponToSet);
        else StartComboHitCheck();
    }

    private void SetPlayerWeapon(Weapon weaponToSet)
    {
        currentWeapon = weaponToSet;
    }

    private void DifferentWeapons(Weapon weaponToSet)
    {
        if (GetHitOver())
        {
            EndCombo();
            SetPlayerWeapon(weaponToSet);
            StartComboHitCheck();
        }
    }

    protected override void Start()
    {
        SetPlayerWeapon(playerMainWeapon);
        base.Start();
    }
}

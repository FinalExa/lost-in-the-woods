using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPayCombo : Combo
{
    [SerializeField] private Weapon weaponToSet;

    protected override void Start()
    {
        SetWeapon(weaponToSet);
        base.Start();
    }

    public void ExecuteLightPayCombo()
    {
        StartComboHitCheck();
    }

    public void StopLightPayCombo()
    {
        if (comboActive) EndCombo();
    }
}

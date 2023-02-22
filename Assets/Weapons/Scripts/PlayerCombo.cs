using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : Combo
{
    [SerializeField] private Weapon playerMainWeapon;
    [SerializeField] private Weapon playerSecondaryWeapon;

    public void SetPlayerWeapon(bool isSecondary)
    {
        if (!isSecondary) currentWeapon = playerMainWeapon;
        else currentWeapon = playerSecondaryWeapon;
    }

    protected override void Start()
    {
        SetPlayerWeapon(false);
        base.Start();
    }
}

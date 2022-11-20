using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCombo : Combo
{
    [SerializeField] private Weapon weaponToSet;
    protected override void Start()
    {
        base.Start();
        SetWeapon(weaponToSet);
    }
    public override void Update()
    {
        base.Update();
        AutoComboExecute();
    }
    private void AutoComboExecute()
    {
        if (comboHitOver && !comboDelay) StartComboHit();
    }
}

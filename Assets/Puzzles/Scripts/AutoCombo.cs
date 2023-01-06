using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCombo : Combo
{
    [SerializeField] private Weapon weaponToSet;
    [SerializeField] private bool resetsPositionAndRotationOnComboEnd;
    private Vector3 startPosition;
    private Quaternion startRotation;
    protected override void Start()
    {
        base.Start();
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
        SetWeapon(weaponToSet);
    }
    public override void Update()
    {
        base.Update();
        AutoComboExecute();
    }
    private void AutoComboExecute()
    {
        StartComboHitCheck();
    }

    public override void OnComboEnd()
    {
        if (resetsPositionAndRotationOnComboEnd) ResetPositionAndRotation();
    }

    private void ResetPositionAndRotation()
    {
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }
}

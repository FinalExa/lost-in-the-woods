using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoCombo : Combo
{
    [SerializeField] private Weapon weaponToSet;
    [SerializeField] private bool resetsPositionAndRotationOnComboEnd;
    [SerializeField] private bool destroyObjectOnComboEnd;
    private Vector3 startPosition;
    private Quaternion startRotation;
    protected override void Start()
    {
        base.Start();
        startPosition = this.transform.position;
        startRotation = this.transform.rotation;
        SetWeapon(weaponToSet);
    }
    public void Update()
    {
        AutoComboExecute();
    }
    private void AutoComboExecute()
    {
        StartComboHitCheck();
    }

    public override void OnComboEnd()
    {
        if (resetsPositionAndRotationOnComboEnd) ResetPositionAndRotation();
        if (destroyObjectOnComboEnd) GameObject.Destroy(this.gameObject);
    }

    private void ResetPositionAndRotation()
    {
        this.transform.position = startPosition;
        this.transform.rotation = startRotation;
    }
}

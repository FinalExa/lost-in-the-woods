using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPayCombo : Combo
{
    [SerializeField] private Weapon weaponToSet;
    [HideInInspector] public AffectedByLight affectedByLight;

    protected override void Awake()
    {
        base.Awake();
        affectedByLight = this.gameObject.GetComponent<AffectedByLight>();
    }

    protected override void Start()
    {
        SetWeapon(weaponToSet);
        base.Start();
    }

    public void ExecuteLightPayCombo()
    {
        if (currentWeapon != null) StartComboHitCheck();
    }

    public void StopLightPayCombo()
    {
        if (comboActive) EndCombo();
    }
}

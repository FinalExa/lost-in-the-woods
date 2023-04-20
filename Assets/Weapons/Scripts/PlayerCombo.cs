using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : Combo
{
    [SerializeField] private Weapon playerMainWeapon;
    [SerializeField] private Weapon playerSecondaryWeapon;
    [HideInInspector] public bool pcLockedAttack;

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

    public IEnumerator LockPlayerAttack(float timeToWait)
    {
        pcLockedAttack = true;
        yield return new WaitForSeconds(timeToWait);
        pcLockedAttack = false;
    }

    protected override void Start()
    {
        SetPlayerWeapon(playerMainWeapon);
        base.Start();
    }
}

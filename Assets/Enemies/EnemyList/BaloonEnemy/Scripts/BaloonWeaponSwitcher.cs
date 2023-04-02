using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonWeaponSwitcher : EnemyWeaponSwitcher
{
    private BaloonEnemyController baloonEnemyController;
    public struct BaloonWeaponSet
    {
        public WeaponAttack.WeaponAttackType weaponAttackType;
        public string namedInteractionName;
        public Weapon normalWeapon;
        public Weapon calmWeapon;
        public Weapon berserkWeapon;
    }
    private BaloonWeaponSet nothingAbsorbedWeapons;
    private List<BaloonWeaponSet> absorbedWeapons;

    protected override void Startup()
    {
        base.Startup();
        baloonEnemyController = (BaloonEnemyController)enemyController;
        SetBaseWeapon();
        SetExtraWeapons();
    }
    private void SetBaseWeapon()
    {
        nothingAbsorbedWeapons = new BaloonWeaponSet();
        nothingAbsorbedWeapons.weaponAttackType = WeaponAttack.WeaponAttackType.GENERIC;
        nothingAbsorbedWeapons.namedInteractionName = string.Empty;
        nothingAbsorbedWeapons.normalWeapon = normalStateWeapon;
        nothingAbsorbedWeapons.calmWeapon = calmStateWeapon;
        nothingAbsorbedWeapons.berserkWeapon = berserkStateWeapon;
    }

    private void SetExtraWeapons()
    {
        absorbedWeapons = new List<BaloonWeaponSet>();
        foreach (BaloonEnemyController.AbsorbableTypes absorbableType in baloonEnemyController.absorbableTypes)
        {
            BaloonWeaponSet baloonWeaponSet = new BaloonWeaponSet();
            baloonWeaponSet.weaponAttackType = absorbableType.absorbableAttackType;
            baloonWeaponSet.namedInteractionName = absorbableType.absorbableName;
            baloonWeaponSet.normalWeapon = GenerateWeapon(absorbableType.thisTypeNormalWeapon);
            baloonWeaponSet.calmWeapon = GenerateWeapon(absorbableType.thisTypeCalmWeapon);
            baloonWeaponSet.berserkWeapon = GenerateWeapon(absorbableType.thisTypeBerserkWeapon);
            absorbedWeapons.Add(baloonWeaponSet);
        }
    }

    public void SetWeapons(WeaponAttack.WeaponAttackType receivedType)
    {
        if (receivedType == WeaponAttack.WeaponAttackType.GENERIC) ChangeActiveWeapons(nothingAbsorbedWeapons.normalWeapon, nothingAbsorbedWeapons.calmWeapon, nothingAbsorbedWeapons.berserkWeapon);
        else
        {
            foreach (BaloonWeaponSet baloonWeaponSet in absorbedWeapons)
            {
                if (baloonWeaponSet.weaponAttackType == receivedType)
                {
                    ChangeActiveWeapons(baloonWeaponSet.normalWeapon, baloonWeaponSet.calmWeapon, baloonWeaponSet.berserkWeapon);
                    break;
                }
            }
        }
    }

    private void ChangeActiveWeapons(Weapon normal, Weapon calm, Weapon berserk)
    {
        normalStateWeapon = normal;
        calmStateWeapon = calm;
        berserkStateWeapon = berserk;
        SetEnemyWeaponByState();
    }
}

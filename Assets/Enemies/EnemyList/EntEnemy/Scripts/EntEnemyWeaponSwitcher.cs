using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntEnemyWeaponSwitcher : EnemyWeaponSwitcher
{
    private EntEnemyController entEnemyController;
    [HideInInspector] public Weapon normalVigorousWeapon;
    [HideInInspector] public Weapon calmVigorousWeapon;
    [HideInInspector] public Weapon berserkVigorousWeapon;
    [HideInInspector] public Weapon normalDepletedWeapon;
    [HideInInspector] public Weapon calmDepletedWeapon;
    [HideInInspector] public Weapon berserkDepletedWeapon;

    protected override void GenerateEnemyWeapons()
    {
        entEnemyController = (EntEnemyController)enemyController;
        if (entEnemyController.vigorousData.hasNormalWeapon) normalVigorousWeapon = GenerateWeapon(entEnemyController.vigorousData.normalWeapon);
        if (entEnemyController.vigorousData.hasCalmWeapon) calmVigorousWeapon = GenerateWeapon(entEnemyController.vigorousData.calmWeapon);
        if (entEnemyController.vigorousData.hasBerserkWeapon) berserkVigorousWeapon = GenerateWeapon(entEnemyController.vigorousData.berserkWeapon);
        if (entEnemyController.depletedData.hasNormalWeapon) normalDepletedWeapon = GenerateWeapon(entEnemyController.depletedData.normalWeapon);
        if (entEnemyController.depletedData.hasCalmWeapon) calmDepletedWeapon = GenerateWeapon(entEnemyController.depletedData.calmWeapon);
        if (entEnemyController.depletedData.hasBerserkWeapon) berserkDepletedWeapon = GenerateWeapon(entEnemyController.depletedData.berserkWeapon);
        SwitchEntWeapons();
    }

    public void SwitchEntWeapons()
    {
        if (entEnemyController.enemyData == entEnemyController.vigorousData) SetWeapons(normalVigorousWeapon, calmVigorousWeapon, berserkVigorousWeapon);
        else SetWeapons(normalDepletedWeapon, calmDepletedWeapon, berserkDepletedWeapon);
    }

    private void SetWeapons(Weapon normal, Weapon calm, Weapon berserk)
    {
        normalStateWeapon = normal;
        calmStateWeapon = calm;
        berserkStateWeapon = berserk;
    }
}

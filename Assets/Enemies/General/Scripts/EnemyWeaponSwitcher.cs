using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSwitcher : MonoBehaviour
{
    private EnemyController enemyController;
    public Weapon normalStateWeapon;
    public Weapon calmStateWeapon;
    public Weapon berserkStateWeapon;
    [SerializeField] private GameObject enemyWeaponsSlot;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }

    private void Start()
    {
        GenerateEnemyWeapons();
    }

    private void GenerateEnemyWeapons()
    {
        Weapon weapon;
        if (enemyController.enemyData.hasNormalWeapon)
        {
            weapon = Instantiate(enemyController.enemyData.normalWeapon, enemyWeaponsSlot.transform);
            normalStateWeapon = weapon;
        }
        if (enemyController.enemyData.hasCalmWeapon)
        {
            weapon = Instantiate(enemyController.enemyData.calmWeapon, enemyWeaponsSlot.transform);
            calmStateWeapon = weapon;
        }
        if (enemyController.enemyData.hasBerserkWeapon)
        {
            weapon = Instantiate(enemyController.enemyData.berserkWeapon, enemyWeaponsSlot.transform);
            berserkStateWeapon = weapon;
        }
        SetEnemyWeaponByState();
    }

    public void SetEnemyWeaponByState()
    {
        switch (enemyController.enemyLightState)
        {
            case EnemyController.EnemyLightState.NORMAL:
                if (enemyController.enemyData.hasNormalWeapon) SetEnemyWeapon(normalStateWeapon);
                break;
            case EnemyController.EnemyLightState.CALM:
                if (enemyController.enemyData.hasCalmWeapon) SetEnemyWeapon(calmStateWeapon);
                break;
            case EnemyController.EnemyLightState.BERSERK:
                if (enemyController.enemyData.hasBerserkWeapon) SetEnemyWeapon(berserkStateWeapon);
                break;
        }
    }
    private void SetEnemyWeapon(Weapon weaponToSet)
    {
        enemyController.currentWeapon = weaponToSet;
        enemyController.enemyCombo.SetWeapon(enemyController.currentWeapon);
    }
}

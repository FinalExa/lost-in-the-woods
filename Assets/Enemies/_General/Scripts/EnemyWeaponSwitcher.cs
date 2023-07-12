using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponSwitcher : MonoBehaviour
{
    protected EnemyController enemyController;
    public Weapon normalStateWeapon;
    public Weapon calmStateWeapon;
    public Weapon berserkStateWeapon;
    [SerializeField] protected GameObject enemyWeaponsSlot;

    private void Awake()
    {
        Startup();
    }
    private void Start()
    {
        SetEnemyWeaponByState();
    }

    protected virtual void Startup()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
        GenerateEnemyWeapons();
    }
    protected virtual void GenerateEnemyWeapons()
    {
        if (enemyController.enemyData.hasNormalWeapon) normalStateWeapon = GenerateWeapon(enemyController.enemyData.normalWeapon);
        if (enemyController.enemyData.hasCalmWeapon) calmStateWeapon = GenerateWeapon(enemyController.enemyData.calmWeapon);
        if (enemyController.enemyData.hasBerserkWeapon) berserkStateWeapon = GenerateWeapon(enemyController.enemyData.berserkWeapon);
    }
    protected Weapon GenerateWeapon(Weapon weaponRef)
    {
        return Instantiate(weaponRef, enemyWeaponsSlot.transform);
    }
    public virtual void SetEnemyWeaponByState()
    {
        if (enemyController.affectedByLight.lightState == AffectedByLight.LightState.NORMAL && enemyController.enemyData.hasNormalWeapon) SetEnemyWeapon(normalStateWeapon);
        else if (enemyController.affectedByLight.lightState == AffectedByLight.LightState.CALM && enemyController.enemyData.hasCalmWeapon) SetEnemyWeapon(calmStateWeapon);
        else if (enemyController.affectedByLight.lightState == AffectedByLight.LightState.BERSERK && enemyController.enemyData.hasBerserkWeapon) SetEnemyWeapon(berserkStateWeapon);
    }
    protected void SetEnemyWeapon(Weapon weaponToSet)
    {
        enemyController.currentWeapon = weaponToSet;
        enemyController.enemyCombo.SetWeapon(enemyController.currentWeapon);
    }
}

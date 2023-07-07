using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntEnemyController : EnemyController, ISendSignalToSelf
{
    private EntEnemyWeaponSwitcher entEnemyWeaponSwitcher;
    public EnemyData vigorousData;
    [SerializeField] private string swapToVigorousName;
    public EnemyData depletedData;
    [SerializeField] private string swapToDepletedName;
    [HideInInspector] public bool stunned;
    [SerializeField] private float stunTotalTime;
    private float stunTimer;

    protected override void Awake()
    {
        base.Awake();
        entEnemyWeaponSwitcher = this.gameObject.GetComponent<EntEnemyWeaponSwitcher>();
    }

    private void Update()
    {
        StunTimer();
    }

    public void OnSignalReceived(GameObject source)
    {
        EntCheckForSwap();
    }

    private void StunTimer()
    {
        if (stunned)
        {
            if (stunTimer > 0) stunTimer -= Time.deltaTime;
            else stunned = false;
        }
    }

    private void EntCheckForSwap()
    {
        if (InteractionContainsName(swapToVigorousName) && enemyData != vigorousData) SetStunned(vigorousData);
        else if (InteractionContainsName(swapToDepletedName) && enemyData != depletedData) SetStunned(depletedData);
    }

    private void SetStunned(EnemyData newData)
    {
        enemyCombo.EndCombo();
        stunned = true;
        stunTimer = stunTotalTime;
        enemyData = newData;
        entEnemyWeaponSwitcher.SwitchEntWeapons();
    }
}

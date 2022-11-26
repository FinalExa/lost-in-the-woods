using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyController enemyController;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }

    private void OnEnable()
    {
        SetHPStartup(enemyController.enemyData.maxHP);
    }

    public override void OnDeath()
    {
        enemyController.spawnerRef.SetEnemyDead(enemyController.spawnerEnemyInfo);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    private EnemyController enemyController;
    private AttackInteraction attackInteraction;
    private bool deathDone;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
        attackInteraction = this.gameObject.GetComponent<AttackInteraction>();
    }

    private void OnEnable()
    {
        SetHPStartup(enemyController.enemyData.maxHP);
        deathDone = false;
    }

    public override void OnDeath()
    {
        if (!deathDone)
        {
            enemyController.spawnerRef.SetEnemyDead(enemyController.spawnerEnemyInfo);
            if (attackInteraction != null) attackInteraction.OnDeathInteraction();
        }
    }

    private void OnDisable()
    {
        OnDeath();
    }
}

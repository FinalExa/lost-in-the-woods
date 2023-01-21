using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected EnemyController enemyController;
    protected AttackInteraction attackInteraction;
    protected bool deathDone;

    protected virtual void Awake()
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
        OnDeathSound();
        OnDeathInteraction();
        SetEnemyDead();
    }
    private void SetEnemyDead()
    {
        if (!deathDone)
        {
            if (enemyController.spawnerRef != null) enemyController.spawnerRef.SetEnemyDead(enemyController.spawnerEnemyInfo);
            else this.gameObject.SetActive(false);
        }
    }

    protected virtual void OnDeathInteraction()
    {
        if (!deathDone && attackInteraction != null) attackInteraction.OnDeathInteraction();
    }

    private void OnDisable()
    {
        OnDeath();
    }
}

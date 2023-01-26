using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    protected EnemyController enemyController;
    protected Interaction interaction;
    protected bool deathDone;

    protected virtual void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
        interaction = this.gameObject.GetComponent<Interaction>();
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
            if (enemyController.spawnerRef != null) enemyController.spawnerRef.SetEnemyDead(enemyController.spawnerEnemyInfo, false);
            else this.gameObject.SetActive(false);
        }
    }

    protected virtual void OnDeathInteraction()
    {
        if (!deathDone && interaction != null) interaction.OnDeathInteraction();
    }

    private void OnDisable()
    {
        OnDeath();
    }
}

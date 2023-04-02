using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private bool doesntTurnOffOnDeath;
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

    public override void OnDeath(bool skipOnDeathInteraction)
    {
        OnDeathSound();
        if (!doesntTurnOffOnDeath && !skipOnDeathInteraction) OnDeathInteraction();
        if (!doesntTurnOffOnDeath) SetEnemyDead();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnsLifetimeObjOnDeathHealth : EnemyHealth
{
    protected override void OnDeathInteraction()
    {
        if (!deathDone && attackInteraction != null) attackInteraction.OnDeathInteraction(enemyController.spawnerEnemyInfo.maxTimer);
    }
}

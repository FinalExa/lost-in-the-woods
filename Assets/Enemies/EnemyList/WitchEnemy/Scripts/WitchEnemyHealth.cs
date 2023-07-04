
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchEnemyHealth : EnemyHealth
{
    private WitchEnemyController witchEnemyController;

    protected override void Awake()
    {
        base.Awake();
        witchEnemyController = this.gameObject.GetComponent<WitchEnemyController>();
    }

    public override void HealthAddValue(float healthToAdd, bool feedbackActive)
    {
        if (healthToAdd < 0) witchEnemyController.witchHidden.LeaveHiddenState();
        base.HealthAddValue(healthToAdd, feedbackActive);
    }
}

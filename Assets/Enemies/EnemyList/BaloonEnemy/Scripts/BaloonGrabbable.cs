using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonGrabbable : GrabbableByPlayer
{
    private BaloonEnemyController baloonEnemyController;

    protected override void Awake()
    {
        base.Awake();
        baloonEnemyController = this.gameObject.GetComponent<BaloonEnemyController>();
    }

    public override void MainOperation(PCGrabbing pcGrabbing, Vector3 direction, float speed)
    {
        baloonEnemyController.enemyCombo.ActivateEnemyCombo();
    }
}

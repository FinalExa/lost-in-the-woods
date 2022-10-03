using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : Combo
{
    private EnemyController enemyController;
    [HideInInspector] public bool isInCombo;
    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isInCombo) EnemyAutoCombo();
    }

    public void ActivateEnemyCombo()
    {
        isInCombo = true;
        enemyController.enemyRotator.Rotate();
    }

    private void EnemyAutoCombo()
    {
        if (comboHitOver && !comboDelay) StartComboHit();
    }

    protected override void OnComboEnd()
    {
        isInCombo = false;
    }
}

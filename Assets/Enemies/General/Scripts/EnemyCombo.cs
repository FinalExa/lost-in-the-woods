using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : Combo
{
    private EnemyController enemyController;
    [HideInInspector] public bool isInCombo;
    [HideInInspector] public Transform target;
    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }
    protected override void Start()
    {
        base.Start();
        target = enemyController.playerTarget.transform;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        if (isInCombo) EnemyAutoCombo();
        if (!isAttacking) Direction();
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
    private void Direction()
    {
        lastDirection = (target.position - this.transform.position).normalized;
    }
}

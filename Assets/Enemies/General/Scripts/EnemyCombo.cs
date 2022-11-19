using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : Combo
{
    private EnemyRotator enemyRotator;
    [HideInInspector] public bool isInCombo;
    [HideInInspector] public Transform target;
    private void Awake()
    {
        enemyRotator = this.gameObject.GetComponent<EnemyRotator>();
    }
    protected override void Start()
    {
        base.Start();
        target = this.gameObject.GetComponent<EnemyController>().playerTarget.transform;
    }

    public override void Update()
    {
        base.Update();
        if (isInCombo) EnemyAutoCombo();
        if (!isAttacking) Direction();
    }

    public void ActivateEnemyCombo()
    {
        isInCombo = true;
        enemyRotator.Rotate();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : Combo
{
    private EnemyRotator enemyRotator;
    private EnemyController enemyController;
    [HideInInspector] public bool isInCombo;
    [HideInInspector] public Transform target;
    private void Awake()
    {
        enemyRotator = this.gameObject.GetComponent<EnemyRotator>();
        enemyController = this.gameObject.GetComponent<EnemyController>();
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
        else if (!isAttacking) Direction(target.position);
    }

    public void ActivateEnemyCombo(Vector3 target)
    {
        isInCombo = true;
        enemyRotator.Rotate(target);
        Direction(target);
    }

    private void EnemyAutoCombo()
    {
        if (comboHitOver && !comboDelay) StartComboHit();
    }

    protected override void OnComboEnd()
    {
        isInCombo = false;
        enemyController.attackDone = true;
    }

    public void Direction(Vector3 target)
    {
        lastDirection = (target - this.transform.position).normalized;
    }
}

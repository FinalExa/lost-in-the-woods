using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombo : Combo
{
    private EnemyRotator enemyRotator;
    private EnemyController enemyController;
    [HideInInspector] public bool isInCombo;
    [HideInInspector] public Transform target;
    protected override void Awake()
    {
        base.Awake();
        enemyRotator = this.gameObject.GetComponent<EnemyRotator>();
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }
    protected override void Start()
    {
        base.Start();
        target = this.gameObject.GetComponent<EnemyController>().playerTarget.transform;
    }

    private void Update()
    {
        if (isInCombo) EnemyAutoCombo();
        else if (!GetIfIsAttacking()) Direction(target.position);
    }

    public void ActivateEnemyCombo(Vector3 target)
    {
        isInCombo = true;
        enemyRotator.Rotate(target);
        Direction(target);
    }

    public void ActivateEnemyCombo()
    {
        isInCombo = true;
    }

    private void EnemyAutoCombo()
    {
        StartComboHitCheck();
    }

    public override void OnComboEnd()
    {
        isInCombo = false;
        enemyController.AttackDone = true;
    }

    public void Direction(Vector3 target)
    {
        LastDirection = (target - this.transform.position).normalized;
    }
}

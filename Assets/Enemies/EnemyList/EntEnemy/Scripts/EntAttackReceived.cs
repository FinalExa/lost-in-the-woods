using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntAttackReceived : AttackReceived
{
    private EntEnemyController entEnemyController;
    [SerializeField] private float damageMultOnStun;

    protected override void Awake()
    {
        base.Awake();
        entEnemyController = this.gameObject.GetComponent<EntEnemyController>();
    }

    public override void DealDamage(bool invulnerable, float damage)
    {
        if (entEnemyController.stunned) damage *= damageMultOnStun;
        base.DealDamage(invulnerable, damage);
    }
}

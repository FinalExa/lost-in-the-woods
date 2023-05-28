using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootShieldAttackReceived : AttackReceived
{
    RootShieldEnemyController rootShieldEnemyController;

    protected override void Awake()
    {
        base.Awake();
        rootShieldEnemyController = this.gameObject.GetComponent<RootShieldEnemyController>();
    }

    private void Update()
    {
        if (rootShieldEnemyController.affectedByLight.lightState == AffectedByLight.LightState.CALM)
        {
            if (!rootShieldEnemyController.shieldUp) DealDamage(false, rootShieldEnemyController.exposedLightDamagePerSecond * Time.deltaTime);
            else DealDamage(false, rootShieldEnemyController.protectedLightDamagePerSecond * Time.deltaTime);
        }
    }

    public override void DealDamage(bool invulnerable, float damage)
    {
        if (!rootShieldEnemyController.shieldUp) base.DealDamage(invulnerable, damage);
        else rootShieldEnemyController.ShieldAddValue(-damage);
    }
}
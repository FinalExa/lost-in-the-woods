using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    [HideInInspector] public bool isVulnerable;
    public GameObject backTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (isVulnerable && other.CompareTag("Hole"))
        {
            other.gameObject.GetComponent<AttackInteraction>().CheckIfEnemyIsTheSame(enemyData.enemyName);
            this.gameObject.GetComponent<EnemyHealth>().OnDeath();
        }
    }

    public override void LightStateChange()
    {
        base.LightStateChange();
        if (enemyLightState == EnemyLightState.NORMAL) isVulnerable = false;
        else isVulnerable = true;
    }
}

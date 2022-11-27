using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    private ShroomTrigger shroomTrigger;
    public GameObject backTrigger;

    protected override void Awake()
    {
        base.Awake();
        shroomTrigger = this.gameObject.GetComponent<ShroomTrigger>();
    }
    public override void LightStateChange()
    {
        base.LightStateChange();
        if (enemyLightState == EnemyLightState.NORMAL) shroomTrigger.isVulnerable = false;
        else shroomTrigger.isVulnerable = true;
    }
}

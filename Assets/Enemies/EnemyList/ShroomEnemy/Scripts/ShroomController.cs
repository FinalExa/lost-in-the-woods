using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    private ShroomCollisions shroomCollisions;
    public GameObject backTrigger;

    protected override void Awake()
    {
        base.Awake();
        shroomCollisions = this.gameObject.GetComponent<ShroomCollisions>();
    }
    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.NORMAL) shroomCollisions.isVulnerable = false;
        else shroomCollisions.isVulnerable = true;
    }
}

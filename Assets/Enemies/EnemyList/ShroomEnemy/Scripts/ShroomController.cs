using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    private NamedInteractionExecutor namedInteraction;

    protected override void Awake()
    {
        base.Awake();
        namedInteraction = this.gameObject.transform.GetComponentInChildren<NamedInteractionExecutor>();
    }

    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.CALM) namedInteraction.active = true;
        else namedInteraction.active = false;
    }
}

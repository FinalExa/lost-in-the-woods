using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    private NamedInteractionExecutor namedInteraction;

    protected override void Awake()
    {
        base.Awake();
        namedInteraction = this.gameObject.GetComponent<NamedInteractionExecutor>();
    }

    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.NORMAL) namedInteraction.active = false;
        else namedInteraction.active = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    private NamedInteraction namedInteraction;

    protected override void Awake()
    {
        base.Awake();
        namedInteraction = this.gameObject.GetComponent<NamedInteraction>();
    }

    private void Update()
    {
        if (namedInteraction.interactionDone) this.gameObject.SetActive(false);
    }

    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.NORMAL) namedInteraction.active = false;
        else namedInteraction.active = true;
    }
}

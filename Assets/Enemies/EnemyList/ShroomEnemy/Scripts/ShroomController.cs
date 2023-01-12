using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShroomController : EnemyController
{
    private GenericNamedInteractionExecutor genericNamedInteractionExecutor;
    public GameObject backTrigger;

    protected override void Awake()
    {
        base.Awake();
        genericNamedInteractionExecutor = this.gameObject.GetComponent<GenericNamedInteractionExecutor>();
    }

    private void Update()
    {
        if (genericNamedInteractionExecutor.interactionDone) this.gameObject.SetActive(false);
    }

    public override void LightStateUpdate()
    {
        if (affectedByLight.lightState == AffectedByLight.LightState.NORMAL) genericNamedInteractionExecutor.active = false;
        else genericNamedInteractionExecutor.active = true;
    }
}

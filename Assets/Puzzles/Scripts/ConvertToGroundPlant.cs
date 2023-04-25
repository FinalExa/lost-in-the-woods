using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToGroundPlant : GrabbableByPlayer
{
    private Interaction interaction;
    protected override void Awake()
    {
        base.Awake();
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    public override void MainOperation(PCGrabbing pcGrabbing, Vector3 direction, float speed)
    {
        ReleaseFromBeingGrabbed();
        interaction.ExecuteCallByCodeInteraction();
    }
}

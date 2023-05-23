using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedGrabbable : GrabbableByPlayer
{
    private Seed seedRef;
    private SeedPillar referencedNestledArea;
    private bool isInNestledInPillar;

    protected override void Awake()
    {
        base.Awake();
        seedRef = this.gameObject.GetComponent<Seed>();
    }

    public override void SetGrabbedByPlayer()
    {
        if (isInNestledInPillar) referencedNestledArea.RemoveSeed();
        base.SetGrabbedByPlayer();
    }

    public override void MainOperation(PCGrabbing pcGrabbing, Vector3 direction, float speed)
    {
        if (referencedNestledArea == null) base.MainOperation(pcGrabbing, direction, speed);
        else
        {
            isInNestledInPillar = true;
            ReleaseFromBeingGrabbed();
            referencedNestledArea.SetSeed(seedRef);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SeedPillar")) referencedNestledArea = other.gameObject.GetComponent<SeedPillar>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("SeedPillar")) referencedNestledArea = null;
    }
}

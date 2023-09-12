using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertToGroundPlant : GrabbableByPlayer
{
    private Interaction interaction;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float downOffset;
    [SerializeField] private float halfExtent;
    protected override void Awake()
    {
        base.Awake();
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    public override void MainOperation(PCGrabbing pcGrabbing, Vector3 direction, float speed)
    {
        if (CheckForGroundBelow())
        {
            if (onMainOperationUX.hasSound) onMainOperationUX.sound.PlayAudio();
            ReleaseFromBeingGrabbed();
            interaction.ExecuteCallByCodeInteraction();
        }
    }

    private bool CheckForGroundBelow()
    {
        Vector3 downPos = this.transform.position - new Vector3(0f, downOffset, 0f);
        Collider[] collidersBelow = Physics.OverlapBox(downPos, new Vector3(0.05f, halfExtent, 0.05f));
        foreach (Collider colliderBelow in collidersBelow)
        {
            if (IsInGround(colliderBelow.gameObject, groundLayer)) return true;
        }
        return false;
    }

    private bool IsInGround(GameObject obj, LayerMask layerMask)
    {
        return (((layerMask.value & (1 << obj.layer)) > 0) && (obj.CompareTag("Ground")));
    }
}

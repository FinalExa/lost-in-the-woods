using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCFall : MonoBehaviour
{
    private PCReferences pcReferences;
    private bool touchingGround;
    private Vector3 lastGroundPosition;
    [SerializeField] private CapsuleCollider playerCapsuleCollider;
    [SerializeField] private GameObject fallTarget;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 overlapBoxHalfExtents;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        lastGroundPosition = this.transform.position;
    }

    private void FixedUpdate()
    {
        CheckForGroundBelow();
    }

    private void CheckForGroundBelow()
    {
        Collider[] grounds = Physics.OverlapBox(fallTarget.transform.position, overlapBoxHalfExtents, Quaternion.identity, groundMask);
        if (grounds.Length > 0) SetOnGround();
        else SetNotOnGround();
    }

    public void SetLastGround(Vector3 newPosition)
    {
        lastGroundPosition = newPosition;
    }

    private void SetOnGround()
    {
        if (!pcReferences.inputs.enabled) pcReferences.inputs.enabled = true;
        if (!playerCapsuleCollider.enabled) playerCapsuleCollider.enabled = true;
        touchingGround = true;
    }

    private void SetNotOnGround()
    {
        if (pcReferences.inputs.enabled) pcReferences.inputs.enabled = false;
        if (playerCapsuleCollider.enabled) playerCapsuleCollider.enabled = false;
        touchingGround = false;
    }

    public void ReturnToLastGroundPosition()
    {
        if (!touchingGround)
        {
            this.gameObject.transform.position = lastGroundPosition;
            pcReferences.attackReceived.DealDamage(false, pcReferences.pcData.damageOnFall);
            SetOnGround();
        }
    }
}

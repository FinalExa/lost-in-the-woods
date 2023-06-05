using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCFall : MonoBehaviour
{
    private PCReferences pcReferences;
    private bool touchingGround;
    private Vector3 lastGroundPosition;
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
        SetLastGround(grounds);
    }

    private void SetLastGround(Collider[] grounds)
    {
        if (touchingGround)
        {
            foreach (Collider ground in grounds)
            {
                if (ground.CompareTag("Ground"))
                {
                    Vector3 groundPos = ground.gameObject.transform.position;
                    lastGroundPosition = new Vector3(groundPos.x, groundPos.y + ground.gameObject.transform.localScale.y / 2, groundPos.z);
                    break;
                }
            }
        }
    }

    private void SetOnGround()
    {
        touchingGround = true;
    }

    private void SetNotOnGround()
    {
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

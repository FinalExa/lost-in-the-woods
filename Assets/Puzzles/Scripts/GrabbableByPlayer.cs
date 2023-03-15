using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableByPlayer : MonoBehaviour
{
    [HideInInspector] public PCController playerRef;
    [HideInInspector] public Rigidbody thisRb;
    private RigidbodyConstraints defaultConstraints;
    private void Awake()
    {
        playerRef = FindObjectOfType<PCController>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (thisRb != null) defaultConstraints = thisRb.constraints;
    }

    public void ReceivedSecondary()
    {
        playerRef.SetGrabbedObject(this);
    }

    public void SetGrabbed(Transform newParent)
    {
        if (thisRb != null) thisRb.constraints = RigidbodyConstraints.FreezeAll;
        this.gameObject.transform.position = newParent.position;
        this.gameObject.transform.parent = newParent;
    }

    public void ReleaseFromBeingGrabbed()
    {
        if (thisRb != null) thisRb.constraints = defaultConstraints;
        this.gameObject.transform.parent = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableByPlayer : MonoBehaviour
{
    [HideInInspector] public PCController playerRef;
    [HideInInspector] public Rigidbody thisRb;
    private Transform startParent;
    private RigidbodyConstraints defaultConstraints;
    private void Awake()
    {
        playerRef = FindObjectOfType<PCController>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (thisRb != null) defaultConstraints = thisRb.constraints;
        startParent = this.gameObject.transform.parent;
    }

    private void Update()
    {
        ResetPosition();
    }

    private void ResetPosition()
    {
        if (this.gameObject.transform.parent != null && this.gameObject.transform.localPosition != Vector3.zero) this.gameObject.transform.localPosition = Vector3.zero;
    }

    public void ReceivedSecondary()
    {
        playerRef.pcReferences.pcGrabbing.SetGrabbedObject(this);
    }

    public void SetGrabbed(GameObject newParent)
    {
        if (thisRb != null)
        {
            thisRb.velocity = Vector3.zero;
            thisRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        this.gameObject.transform.position = newParent.transform.position;
        this.gameObject.transform.parent = newParent.transform;
    }

    public void ReleaseFromBeingGrabbed()
    {
        if (thisRb != null)
        {
            thisRb.constraints = defaultConstraints;
        }
        this.gameObject.transform.parent = startParent;
    }
}
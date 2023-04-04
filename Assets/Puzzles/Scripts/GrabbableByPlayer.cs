using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableByPlayer : MonoBehaviour
{
    [HideInInspector] public PCController playerRef;
    [HideInInspector] public Rigidbody thisRb;
    [HideInInspector] public Transform startParent;
    public bool lockedGrabbable;
    private RigidbodyConstraints defaultConstraints;
    protected virtual void Awake()
    {
        if (playerRef == null) playerRef = FindObjectOfType<PCController>();
        if (thisRb == null) thisRb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (thisRb != null) defaultConstraints = thisRb.constraints;
        if (startParent == null) SetStartParent(this.gameObject.transform.parent);
    }

    public void ManualStartup()
    {
        playerRef = FindObjectOfType<PCController>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
        if (thisRb != null) defaultConstraints = thisRb.constraints;
    }

    public void SetStartParent(Transform parent)
    {
        startParent = parent;
    }

    private void Update()
    {
        UpdateOperations();
    }

    private void UpdateOperations()
    {
        if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.gameObject.CompareTag("PlayerGrab"))
        {
            if (this.gameObject.transform.localPosition != Vector3.zero) this.gameObject.transform.localPosition = Vector3.zero;
        }
    }

    public void ReceivedSecondary()
    {
        playerRef.pcReferences.pcGrabbing.SetGrabbedObject(this);
    }

    public void SetGrabbed(GameObject newParent)
    {
        if (!lockedGrabbable)
        {
            if (thisRb != null)
            {
                thisRb.velocity = Vector3.zero;
                thisRb.constraints = RigidbodyConstraints.FreezeAll;
            }
            this.gameObject.transform.position = newParent.transform.position;
            this.gameObject.transform.parent = newParent.transform;
        }
    }

    public void ReleaseFromBeingGrabbed(PCGrabbing pcGrabbing)
    {
        if (thisRb != null)
        {
            thisRb.constraints = defaultConstraints;
        }
        this.gameObject.transform.parent = startParent;
        pcGrabbing.SetGrabbedObjectNull();
    }

    public void ReleaseFromBeingGrabbed()
    {
        if (thisRb != null)
        {
            thisRb.constraints = defaultConstraints;
        }
        this.gameObject.transform.parent = startParent;
    }

    public virtual void MainOperation(PCGrabbing pcGrabbing, Vector3 direction, float speed)
    {
        ReleaseFromBeingGrabbed(pcGrabbing);
        thisRb.velocity = direction * speed;
    }
}
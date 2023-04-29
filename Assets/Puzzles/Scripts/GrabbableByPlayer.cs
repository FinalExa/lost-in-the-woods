using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableByPlayer : MonoBehaviour
{
    [HideInInspector] public PCController playerRef;
    [HideInInspector] public Rigidbody thisRb;
    [HideInInspector] public Transform startParent;
    [HideInInspector] public bool needsToBeGrabbedAgainByPlayer;
    public bool lockedGrabbable;
    private RigidbodyConstraints defaultConstraints;
    private bool defaultGravityActive;
    private Zone zoneRef;
    private List<GameObject> parentOrder;
    private bool parentOrderEnabled;
    private PCGrabbing pcGrabbing;
    protected virtual void Awake()
    {
        ZonePuzzle.deactivatingPuzzleObject += CheckForActiveParent;
        if (playerRef == null) playerRef = FindObjectOfType<PCController>();
        if (thisRb == null) thisRb = this.gameObject.GetComponent<Rigidbody>();
        pcGrabbing = FindObjectOfType<PCGrabbing>();
        zoneRef = this.gameObject.transform.GetComponentInParent<Zone>();
        SetupParentOrder();
    }

    private void Start()
    {
        if (thisRb != null) defaultConstraints = thisRb.constraints;
        if (startParent == null) SetStartParent(this.gameObject.transform.parent);
    }

    private void SetupParentOrder()
    {
        if (zoneRef != null)
        {
            parentOrder = new List<GameObject>();
            parentOrderEnabled = true;
            Transform parent = this.gameObject.transform.parent;
            while (parent.gameObject != zoneRef.gameObject)
            {
                parentOrder.Add(parent.gameObject);
                parent = parent.parent;
            }
        }
    }

    public void SetGrabbedByPlayer()
    {
        playerRef.pcReferences.pcGrabbing.SetGrabbedObject(this);
        needsToBeGrabbedAgainByPlayer = false;
    }

    public void ManualStartup()
    {
        playerRef = FindObjectOfType<PCController>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
        if (thisRb != null)
        {
            defaultConstraints = thisRb.constraints;
            defaultGravityActive = thisRb.useGravity;
        }
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
        if (needsToBeGrabbedAgainByPlayer) SetGrabbedByPlayer();
        RepositionWhileGrabbed();
    }

    private void RepositionWhileGrabbed()
    {
        if (this.gameObject.transform.parent != null && this.gameObject.transform.parent.gameObject.CompareTag("PlayerGrab"))
        {
            if (this.gameObject.transform.localPosition != Vector3.zero) this.gameObject.transform.localPosition = Vector3.zero;
        }
    }

    private void CheckForActiveParent(Zone receivedZone, GameObject receivedObject)
    {
        if (parentOrderEnabled && receivedZone != null && receivedZone == zoneRef)
        {
            foreach (GameObject parent in parentOrder)
            {
                if (parent == receivedObject)
                {
                    pcGrabbing.RemoveGrabbedObject(false);
                    break;
                }
            }
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
                thisRb.useGravity = false;
            }
            this.gameObject.transform.position = newParent.transform.position;
            this.gameObject.transform.parent = newParent.transform;
        }
    }

    public void ReleaseFromBeingGrabbed()
    {
        if (pcGrabbing.grabbedObject == this) pcGrabbing.SetGrabbedObjectNull();
        if (thisRb != null)
        {
            thisRb.constraints = defaultConstraints;
            thisRb.useGravity = defaultGravityActive;
        }
        this.gameObject.transform.parent = startParent;
    }

    public virtual void MainOperation(PCGrabbing pcGrabbing, Vector3 direction, float speed)
    {
        ReleaseFromBeingGrabbed();
        thisRb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        DeactivateByFall(other.gameObject);
    }

    protected virtual void DeactivateByFall(GameObject other)
    {
        if (other.CompareTag("FallenZone") && this.gameObject.transform.parent == startParent)
        {
            ObjectEnd();
        }
    }

    private void ObjectEnd()
    {
        if (this.gameObject.GetComponent<EnemyController>() == null) GameObject.Destroy(this.gameObject);
        else this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCGrabbing : MonoBehaviour
{
    private PCController pcController;
    [SerializeField] private GameObject grabPosition;
    private GrabbableByPlayer grabbedObject;

    private void Awake()
    {
        pcController = this.gameObject.GetComponent<PCController>();
    }

    private void Update()
    {
        CheckForRemnants();
    }

    private void CheckForRemnants()
    {
        if (grabbedObject == null && grabPosition.transform.childCount > 0)
        {
            GrabbableByPlayer grabbableByPlayer = grabPosition.transform.GetChild(0).gameObject.GetComponent<GrabbableByPlayer>();
            if (grabbableByPlayer != null) SetGrabbedObject(grabbableByPlayer);
            else grabPosition.transform.GetChild(0).transform.parent = null;
        }
    }

    public void SetGrabbedObject(GrabbableByPlayer objectToSet)
    {
        if (grabbedObject == null)
        {
            grabbedObject = objectToSet;
            grabbedObject.SetGrabbed(grabPosition);
        }
    }

    public void RemoveGrabbedObject(bool launch)
    {
        if (grabbedObject != null)
        {
            StartCoroutine(pcController.LockPlayerAttack(pcController.pcReferences.pcData.afterLaunchLockAttackTime));
            grabbedObject.ReleaseFromBeingGrabbed();
            if (launch) LaunchGrabbedObject();
            grabbedObject = null;
        }
    }

    private void LaunchGrabbedObject()
    {
        Vector3 directionWithSpeed = (grabPosition.transform.position - this.gameObject.transform.position).normalized;
        directionWithSpeed = directionWithSpeed * pcController.pcReferences.pcData.grabLaunchValue;
        grabbedObject.thisRb.velocity = directionWithSpeed;
    }

    public bool GrabbedObjectExists()
    {
        if (grabbedObject != null) return true;
        else return false;
    }
}

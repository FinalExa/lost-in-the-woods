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
            List<GameObject> objectsInGrabPosition = new List<GameObject>();
            bool gotGrabbable = false;
            for (int i = 0; i < grabPosition.transform.childCount; i++)
            {
                GameObject currentObject = grabPosition.transform.GetChild(i).gameObject;
                objectsInGrabPosition.Add(currentObject);
            }
            foreach (GameObject objectInGrabPosition in objectsInGrabPosition)
            {
                GrabbableByPlayer grabbableByPlayer = objectInGrabPosition.GetComponent<GrabbableByPlayer>();
                if (!gotGrabbable && grabbableByPlayer != null)
                {
                    SetGrabbedObject(grabbableByPlayer);
                    gotGrabbable = true;
                }
                else objectInGrabPosition.transform.parent = null;
            }
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

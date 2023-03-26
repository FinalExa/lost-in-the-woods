using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public Weapon thisWeapon;
    [HideInInspector] public bool pcLockedAttack;
    [SerializeField] private GameObject grabPosition;
    private GrabbableByPlayer grabbedObject;
    private Zone currentZone;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        CheckStartingZone();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void ChangePlayerZone(Zone zoneToSet)
    {
        if (currentZone != null) currentZone.SetPlayerOutOfZone();
        currentZone = zoneToSet;
    }

    public Zone GetCurrentZone()
    {
        return currentZone;
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
            StartCoroutine(LockPlayerAttack(pcReferences.pcData.afterLaunchLockAttackTime));
            grabbedObject.ReleaseFromBeingGrabbed();
            if (launch) LaunchGrabbedObject();
            grabbedObject = null;
        }
    }

    private void LaunchGrabbedObject()
    {
        Vector3 directionWithSpeed = (grabPosition.transform.position - this.gameObject.transform.position).normalized;
        directionWithSpeed = directionWithSpeed * pcReferences.pcData.grabLaunchValue;
        grabbedObject.thisRb.velocity = directionWithSpeed;
    }

    public bool GrabbedObjectExists()
    {
        if (grabbedObject != null) return true;
        else return false;
    }

    private void CheckStartingZone()
    {
        Collider[] collidersTouchingPlayerAtStart = Physics.OverlapBox(this.transform.position, new Vector3(0.1f, 2f, 0.1f));
        foreach (Collider collider in collidersTouchingPlayerAtStart)
        {
            ZoneGround zoneGround = collider.GetComponent<ZoneGround>();
            if (zoneGround != null) zoneGround.SetPlayerInZone(this.gameObject.GetComponent<Collider>());
        }
    }
    private IEnumerator LockPlayerAttack(float timeToWait)
    {
        pcLockedAttack = true;
        yield return new WaitForSeconds(timeToWait);
        pcLockedAttack = false;
    }
}

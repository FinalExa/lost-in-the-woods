using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public Weapon thisWeapon;
    [SerializeField] private GameObject grabPosition;
    [HideInInspector] public bool pcLockedAttack;
    private GrabbableByPlayer grabbedObject;
    private Zone currentZone;
    private bool touchingGround;
    private Vector3 lastGroundPosition;
    private RigidbodyConstraints playerConstraints;
    [SerializeField] private RigidbodyConstraints fallingConstraints;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        CheckStartingZone();
        lastGroundPosition = this.transform.position;
        playerConstraints = pcReferences.rb.constraints;
        LaunchGroundCheck();
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
            grabbedObject.SetGrabbed(grabPosition.transform);
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

    private void LaunchGroundCheck()
    {
        StartCoroutine(GroundCheck(pcReferences.pcData.groundCheckTime));
    }

    private IEnumerator GroundCheck(float timeInterval)
    {
        if (touchingGround) lastGroundPosition = this.transform.position;
        yield return new WaitForSeconds(timeInterval);
        LaunchGroundCheck();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = true;
            pcReferences.rb.useGravity = false;
            pcReferences.rb.constraints = playerConstraints;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = false;
            pcReferences.rb.useGravity = true;
            pcReferences.rb.constraints = fallingConstraints;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FallenZone")) ReturnToLastGroundPosition();
    }

    public void ReturnToLastGroundPosition()
    {
        this.gameObject.transform.position = lastGroundPosition;
        pcReferences.attackReceived.DealDamage(false, pcReferences.pcData.damageOnFall);
    }
}

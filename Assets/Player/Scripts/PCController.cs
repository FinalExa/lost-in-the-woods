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
    [SerializeField] private GameObject fallTarget;
    [SerializeField] private LayerMask groundMask;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        CheckStartingZone();
        lastGroundPosition = this.transform.position;
        playerConstraints = pcReferences.rb.constraints;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastGroundPosition = new Vector3(collision.gameObject.transform.position.x, 0f, collision.gameObject.transform.position.z);
            if (Physics.Raycast(this.transform.position, fallTarget.transform.position - this.transform.position, out RaycastHit hit, groundMask)) if (!hit.collider.CompareTag("FallenZone")) SetOnGround();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            bool check = true;
            if (Physics.Raycast(this.transform.position, fallTarget.transform.position - this.transform.position, out RaycastHit hit, groundMask)) if (!hit.collider.CompareTag("FallenZone")) check = false;
            if (check) SetNotOnGround();
        }
    }

    private void SetOnGround()
    {
        print("on ground");
        touchingGround = true;
        pcReferences.rb.useGravity = false;
        pcReferences.rb.constraints = playerConstraints;
    }

    private void SetNotOnGround()
    {
        print("not on ground");
        touchingGround = false;
        pcReferences.rb.useGravity = true;
        pcReferences.rb.constraints = fallingConstraints;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FallenZone") && !touchingGround) ReturnToLastGroundPosition();
    }

    public void ReturnToLastGroundPosition()
    {
        this.gameObject.transform.position = lastGroundPosition;
        pcReferences.attackReceived.DealDamage(false, pcReferences.pcData.damageOnFall);
    }
}

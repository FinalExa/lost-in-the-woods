using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCFall : MonoBehaviour
{
    private PCReferences pcReferences;
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
        lastGroundPosition = this.transform.position;
        playerConstraints = pcReferences.rb.constraints;
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
        touchingGround = true;
        pcReferences.rb.useGravity = false;
        pcReferences.rb.constraints = playerConstraints;
    }

    private void SetNotOnGround()
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyStartLaunchForce : MonoBehaviour, IHaveSettableDirection
{
    [SerializeField] private float movementSpeed;
    private Rigidbody thisRb;
    private void Awake()
    {
        thisRb = this.gameObject.GetComponent<Rigidbody>();
        if (thisRb == null) thisRb = this.gameObject.AddComponent<Rigidbody>();
    }
    public void SetDirection(Vector3 receivedDirection)
    {
        thisRb.velocity = receivedDirection * movementSpeed;
    }
}

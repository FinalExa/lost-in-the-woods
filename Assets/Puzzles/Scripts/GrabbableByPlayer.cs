using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableByPlayer : MonoBehaviour
{
    [HideInInspector] public PCController playerRef;
    [HideInInspector] public Rigidbody thisRb;
    private void Awake()
    {
        playerRef = FindObjectOfType<PCController>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
    }

    public void ReceivedSecondary()
    {
        playerRef.SetGrabbedObject(this);
    }
}
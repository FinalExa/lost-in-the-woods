using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCReferences : MonoBehaviour
{
    public PCData pcData;
    public UXEffect uxOnDodge;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Inputs inputs;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public PlayerCombo pcCombo;
    [HideInInspector] public PCRotation pcRotation;
    [HideInInspector] public PCHealth pcHealth;
    [HideInInspector] public AttackReceived attackReceived;
    [HideInInspector] public PCLight pcLight;
    [HideInInspector] public Heartbeat heartbeat;
    [HideInInspector] public PCGrabbing pcGrabbing;
    [HideInInspector] public PCZoneManager pcZoneManager;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        inputs = this.gameObject.GetComponent<Inputs>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        pcCombo = this.gameObject.GetComponent<PlayerCombo>();
        pcRotation = FindObjectOfType<PCRotation>();
        pcHealth = this.gameObject.GetComponent<PCHealth>();
        attackReceived = this.gameObject.GetComponent<AttackReceived>();
        pcLight = this.gameObject.GetComponentInChildren<PCLight>();
        heartbeat = this.gameObject.GetComponent<Heartbeat>();
        pcGrabbing = this.gameObject.GetComponent<PCGrabbing>();
        pcZoneManager = this.gameObject.GetComponent<PCZoneManager>();
        uxOnDodge.UXEffectStartup();
    }
}

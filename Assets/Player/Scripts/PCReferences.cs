using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCReferences : MonoBehaviour
{
    public PCData pcData;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Inputs inputs;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Combo pcCombo;
    [HideInInspector] public PCRotation pcRotation;
    [HideInInspector] public PCHealth pcHealth;
    [HideInInspector] public PCLight pcLight;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        inputs = this.gameObject.GetComponent<Inputs>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        pcCombo = this.gameObject.GetComponent<Combo>();
        pcRotation = FindObjectOfType<PCRotation>();
        pcHealth = this.gameObject.GetComponent<PCHealth>();
        pcLight = this.gameObject.GetComponent<PCLight>();
    }
}

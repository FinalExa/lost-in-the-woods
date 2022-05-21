using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCReferences : MonoBehaviour
{
    public PCData pcData;
    public Light playerLight;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Inputs inputs;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public PCCombo pcCombo;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
        inputs = this.gameObject.GetComponent<Inputs>();
        rb = this.gameObject.GetComponent<Rigidbody>();
        pcCombo = this.gameObject.GetComponent<PCCombo>();
    }
}

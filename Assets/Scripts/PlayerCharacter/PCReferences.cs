using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCReferences : MonoBehaviour
{
    public PCData pcData;
    [HideInInspector] public Camera camera;
    [HideInInspector] public Inputs inputs;
    [HideInInspector] public Rigidbody rigidbody;

    private void Awake()
    {
        camera = FindObjectOfType<Camera>();
        inputs = this.gameObject.GetComponent<Inputs>();
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }
}

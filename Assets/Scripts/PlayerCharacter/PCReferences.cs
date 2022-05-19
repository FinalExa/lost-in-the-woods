using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCReferences : MonoBehaviour
{
    public PCData pcData;
    public Camera camera;
    [HideInInspector] public Inputs inputs;
    [HideInInspector] public Rigidbody rigidbody;

    private void Awake()
    {
        inputs = this.gameObject.GetComponent<Inputs>();
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public float actualSpeed;
    [HideInInspector] public Vector3 lookDirection;
    [HideInInspector] public PCReferences pcReferences;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }

    private void Start()
    {
        lookDirection = pcReferences.pcData.defaultDirection;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCController : MonoBehaviour
{
    [HideInInspector] public string curState;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public float actualSpeed;

    private void Awake()
    {
        pcReferences = this.gameObject.GetComponent<PCReferences>();
    }
}

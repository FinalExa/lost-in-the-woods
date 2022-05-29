using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : MonoBehaviour
{
    [HideInInspector] public BasherReferences basherReferences;

    private void Awake()
    {
        basherReferences = this.gameObject.GetComponent<BasherReferences>();
    }

    private void Start()
    {
        basherReferences.basherNavMesh.isStopped = true;
        basherReferences.basherNavMesh.speed = basherReferences.basherData.defaultMovementSpeed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasherReferences : MonoBehaviour
{
    public BasherData basherData;
    [HideInInspector] public NavMeshAgent basherNavMesh;
    [HideInInspector] public GameObject playerRef;

    private void Awake()
    {
        basherNavMesh = this.gameObject.GetComponent<NavMeshAgent>();
        playerRef = FindObjectOfType<PCController>().gameObject;
    }
}

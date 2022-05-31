using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasherReferences : MonoBehaviour
{
    public BasherData basherData;
    public GameObject damageHitBox;
    [HideInInspector] public NavMeshAgent basherNavMesh;
    [HideInInspector] public GameObject playerRef;
    [HideInInspector] public PCReferences pcReferences;

    private void Awake()
    {
        basherNavMesh = this.gameObject.GetComponent<NavMeshAgent>();
        playerRef = FindObjectOfType<PCController>().gameObject;
        pcReferences = playerRef.GetComponent<PCReferences>();
    }
}

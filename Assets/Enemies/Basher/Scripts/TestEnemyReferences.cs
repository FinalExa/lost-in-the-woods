using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestEnemyReferences : MonoBehaviour
{
    public BasherData basherData;
    public GameObject damageHitBox;
    public Attack attack;
    [HideInInspector] public NavMeshAgent basherNavMesh;
    [HideInInspector] public GameObject playerRef;
    [HideInInspector] public PCReferences pcReferences;
    [HideInInspector] public Health health;
    [HideInInspector] public Combo combo;

    private void Awake()
    {
        basherNavMesh = this.gameObject.GetComponent<NavMeshAgent>();
        playerRef = FindObjectOfType<PCController>().gameObject;
        pcReferences = playerRef.GetComponent<PCReferences>();
        health = this.gameObject.GetComponent<Health>();
        combo = this.gameObject.GetComponent<Combo>();
    }
}

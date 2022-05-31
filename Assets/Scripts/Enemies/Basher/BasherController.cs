using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : MonoBehaviour
{
    [HideInInspector] public BasherReferences basherReferences;
    [HideInInspector] public bool resetAttack;
    public string notDamagingTag;
    public string damagingTag;
    private float actualHealth;

    private void Awake()
    {
        basherReferences = this.gameObject.GetComponent<BasherReferences>();
    }

    private void Start()
    {
        basherReferences.basherNavMesh.isStopped = true;
        basherReferences.basherNavMesh.speed = basherReferences.basherData.defaultMovementSpeed;
        actualHealth = basherReferences.basherData.maxHP;
    }

    public void TakeDamage(float damage)
    {
        actualHealth -= damage;
        if (actualHealth <= 0) this.gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : Controller
{
    [HideInInspector] public BasherReferences basherReferences;
    [HideInInspector] public bool resetAttack;
    public string notDamagingTag;
    public string damagingTag;
    public float attackOffset;

    private void Awake()
    {
        basherReferences = this.gameObject.GetComponent<BasherReferences>();
    }

    private void Start()
    {
        basherReferences.basherNavMesh.isStopped = true;
        basherReferences.basherNavMesh.speed = basherReferences.basherData.defaultMovementSpeed;
        actualHealth = basherReferences.basherData.maxHP;
        hitbox.damageToDeal = basherReferences.basherData.attackDamage;
    }

    public override void HealthAddValue(float value)
    {
        actualHealth += value;
        print(actualHealth);
        if (actualHealth <= 0) this.gameObject.SetActive(false);
    }
}

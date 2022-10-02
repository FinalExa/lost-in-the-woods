using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : Controller
{
    [HideInInspector] public BasherReferences basherReferences;
    [HideInInspector] public bool resetAttack;
    [HideInInspector] public float attackTimer;
    [HideInInspector] public float postAttackTimer;
    public string whoToDamage;
    public float attackOffset;
    public float attackWait;

    private void Awake()
    {
        basherReferences = this.gameObject.GetComponent<BasherReferences>();
    }

    private void Start()
    {
        basherReferences.basherNavMesh.isStopped = true;
        basherReferences.basherNavMesh.speed = basherReferences.basherData.defaultMovementSpeed;
        actualHealth = basherReferences.basherData.maxHP;
        SetupAttack();
        ResetAttackTimer();
        ResetPostAttackTimer();
    }

    public override void HealthAddValue(float value)
    {
        actualHealth += value;
        if (actualHealth <= 0) this.gameObject.SetActive(false);
    }

    public void ResetAttackTimer()
    {
        attackTimer = basherReferences.basherData.attackChargeTime;
    }
    public void ResetPostAttackTimer()
    {
        postAttackTimer = basherReferences.basherData.postAttackTime;
    }

    private void SetupAttack()
    {
        //basherReferences.attack.damageToDeal = basherReferences.basherData.attackDamage;
        basherReferences.attack.whoToDamage = whoToDamage;
    }
}

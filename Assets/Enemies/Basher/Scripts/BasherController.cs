using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasherController : MonoBehaviour
{
    [HideInInspector] public BasherReferences basherReferences;
    [HideInInspector] public bool resetAttack;
    [HideInInspector] public float attackTimer;
    [HideInInspector] public float postAttackTimer;
    [HideInInspector] public Weapon thisWeapon;
    public string whoToDamage;
    public float attackOffset;

    private void Awake()
    {
        basherReferences = this.gameObject.GetComponent<BasherReferences>();
    }

    private void Start()
    {
        basherReferences.basherNavMesh.isStopped = true;
        basherReferences.basherNavMesh.speed = basherReferences.basherData.defaultMovementSpeed;
        basherReferences.health.SetHPStartup(basherReferences.basherData.maxHP);
        SetupWeapon();
        ResetAttackTimer();
        ResetPostAttackTimer();
    }

    public void ResetAttackTimer()
    {
        attackTimer = basherReferences.basherData.attackChargeTime;
    }
    public void ResetPostAttackTimer()
    {
        postAttackTimer = basherReferences.basherData.postAttackTime;
    }

    private void SetupWeapon()
    {
        thisWeapon = this.gameObject.GetComponentInChildren<Weapon>();
        thisWeapon.damageTag = whoToDamage;
    }
}

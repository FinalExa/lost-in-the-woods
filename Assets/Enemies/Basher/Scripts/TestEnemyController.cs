using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyController : MonoBehaviour
{
    [HideInInspector] public TestEnemyReferences testEnemyReferences;
    [HideInInspector] public bool resetAttack;
    [HideInInspector] public float attackTimer;
    [HideInInspector] public float postAttackTimer;
    [HideInInspector] public Weapon thisWeapon;
    public string whoToDamage;
    public float attackOffset;

    private void Awake()
    {
        testEnemyReferences = this.gameObject.GetComponent<TestEnemyReferences>();
    }

    private void Start()
    {
        testEnemyReferences.basherNavMesh.isStopped = true;
        testEnemyReferences.basherNavMesh.speed = testEnemyReferences.basherData.defaultMovementSpeed;
        testEnemyReferences.health.SetHPStartup(testEnemyReferences.basherData.maxHP);
        SetupWeapon();
        ResetAttackTimer();
        ResetPostAttackTimer();
    }

    public void ResetAttackTimer()
    {
        attackTimer = testEnemyReferences.basherData.attackChargeTime;
    }
    public void ResetPostAttackTimer()
    {
        postAttackTimer = testEnemyReferences.basherData.postAttackTime;
    }

    private void SetupWeapon()
    {
        thisWeapon = this.gameObject.GetComponentInChildren<Weapon>();
        thisWeapon.damageTag = whoToDamage;
    }
}

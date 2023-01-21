using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Rotation rotation;
    public EnemyData enemyData;
    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public bool isAlerted;
    [HideInInspector] public GameObject playerTarget;
    [HideInInspector] public EnemyCombo enemyCombo;
    [HideInInspector] public NavMeshAgent thisNavMeshAgent;
    [HideInInspector] public bool attackDone { get; set; }
    [HideInInspector] public Spawner spawnerRef;
    [HideInInspector] public Spawner.EnemiesToRespawn spawnerEnemyInfo;
    [HideInInspector] public AffectedByLight affectedByLight;
    [HideInInspector] public AttackReceived attackReceived;

    protected virtual void Awake()
    {
        playerTarget = FindObjectOfType<PCController>().gameObject;
        enemyCombo = this.gameObject.GetComponent<EnemyCombo>();
        thisNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        affectedByLight = this.gameObject.GetComponent<AffectedByLight>();
        attackReceived = this.gameObject.GetComponent<AttackReceived>();
    }
    public virtual void LightStateUpdate()
    {
        return;
    }
    public void CheckForSwitchState()
    {
        if (affectedByLight.CheckForSwitchState()) attackDone = false;
    }
}

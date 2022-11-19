using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Rotation rotation;
    public EnemyData enemyData;
    public enum EnemyLightState { CALM, NORMAL, BERSERK }
    [HideInInspector] public EnemyLightState enemyLightState;
    [HideInInspector] public Weapon currentWeapon;
    [HideInInspector] public bool isAlerted;
    [HideInInspector] public bool isInsideLight;
    [HideInInspector] public bool isInHeartbeatState;
    [HideInInspector] public GameObject playerTarget;
    [HideInInspector] public EnemyCombo enemyCombo;
    [HideInInspector] public NavMeshAgent thisNavMeshAgent;

    private void Awake()
    {
        playerTarget = FindObjectOfType<PCController>().gameObject;
        enemyCombo = this.gameObject.GetComponent<EnemyCombo>();
        thisNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        Heartbeat.heartbeatSwitch += EnemyHeartbeatState;
        enemyLightState = EnemyLightState.NORMAL;
        isAlerted = true;
    }

    private void EnemyHeartbeatState(bool heartbeatState)
    {
        isInHeartbeatState = heartbeatState;
        LightStateChange();
    }

    public void LightStateChange()
    {
        if (isInsideLight && !isInHeartbeatState) enemyLightState = EnemyLightState.CALM;
        else if (isInHeartbeatState && !isInsideLight) enemyLightState = EnemyLightState.BERSERK;
        else enemyLightState = EnemyLightState.NORMAL;
    }
}

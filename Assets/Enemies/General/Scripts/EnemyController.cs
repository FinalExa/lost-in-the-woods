using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private string whoToDamage;
    [SerializeField] private Weapon enemyWeapon;
    public Rotation rotation;
    public bool isAlerted;
    public float defaultSpeed;
    public float lightUpSpeed;
    public float attackDistance;
    public float lightUpDistance;
    public float lightUpDistanceTolerance;
    public int enemyLightState;
    [HideInInspector] public bool isInsideLight;
    [HideInInspector] public bool isInHeartbeatState;
    [HideInInspector] public GameObject playerTarget;
    [HideInInspector] public EnemyRotator enemyRotator;
    [HideInInspector] public EnemyCombo enemyCombo;
    [HideInInspector] public NavMeshAgent thisNavMeshAgent;
    private void Awake()
    {
        playerTarget = FindObjectOfType<PCController>().gameObject;
        enemyRotator = this.gameObject.GetComponent<EnemyRotator>();
        enemyCombo = this.gameObject.GetComponent<EnemyCombo>();
        thisNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        enemyLightState = 1;
        Heartbeat.heartbeatSwitch += EnemyHeartbeatState;
        isAlerted = true;
        enemyCombo.SetWeapon(enemyWeapon);
    }

    private void EnemyHeartbeatState(bool heartbeatState)
    {
        isInHeartbeatState = heartbeatState;
        LightStateChange();
    }

    public void LightStateChange()
    {
        if (isInsideLight && !isInHeartbeatState) enemyLightState = 0;
        else if (isInHeartbeatState && !isInsideLight) enemyLightState = 2;
        else enemyLightState = 1;
    }
}

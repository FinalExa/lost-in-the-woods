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
    [HideInInspector] public PCController playerRef;
    [HideInInspector] public GameObject playerTarget;
    [HideInInspector] public EnemyCombo enemyCombo;
    [HideInInspector] public NavMeshAgent thisNavMeshAgent;
    [HideInInspector] public bool AttackDone { get; set; }
    [HideInInspector] public Spawner spawnerRef;
    [HideInInspector] public Spawner.EnemiesToRespawn spawnerEnemyInfo;
    [HideInInspector] public AffectedByLight affectedByLight;
    [HideInInspector] public AttackReceived attackReceived;
    [HideInInspector] public Interaction interaction;

    protected virtual void Awake()
    {
        playerRef = FindObjectOfType<PCController>();
        playerTarget = playerRef.gameObject;
        enemyCombo = this.gameObject.GetComponent<EnemyCombo>();
        thisNavMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        affectedByLight = this.gameObject.GetComponent<AffectedByLight>();
        attackReceived = this.gameObject.GetComponent<AttackReceived>();
        interaction = this.gameObject.GetComponent<Interaction>();
    }
    public virtual void LightStateUpdate()
    {
        return;
    }
    public void CheckForSwitchState()
    {
        if (affectedByLight.CheckForSwitchState()) AttackDone = false;
    }

    protected bool InteractionContainsName(string receivedName)
    {
        return interaction.namedInteractionOperations.ActiveNamedInteractions.ContainsKey(receivedName);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonEnemyController : EnemyController
{
    [HideInInspector] public GrabbableByPlayer grabbableByPlayer;
    [HideInInspector] public BaloonRotator baloonRotator;
    [HideInInspector] public EnemyHealth enemyHealth;
    [HideInInspector] public bool vulnerable;


    protected override void Awake()
    {
        base.Awake();
        grabbableByPlayer = this.gameObject.GetComponent<GrabbableByPlayer>();
        enemyHealth = this.gameObject.GetComponent<EnemyHealth>();
        baloonRotator = this.gameObject.GetComponent<BaloonRotator>();
    }

    public void HPDeplete()
    {
        thisNavMeshAgent.enabled = false;
        baloonRotator.followPlayerRotation = true;
        grabbableByPlayer.lockedGrabbable = false;
        vulnerable = true;
    }

    public void HPReset()
    {
        enemyHealth.HealthAddValue(enemyData.maxHP, true);
        thisNavMeshAgent.enabled = true;
        baloonRotator.followPlayerRotation = false;
        grabbableByPlayer.lockedGrabbable = true;
        vulnerable = false;
    }
}

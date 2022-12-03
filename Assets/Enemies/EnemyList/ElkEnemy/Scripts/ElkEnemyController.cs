using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElkEnemyController : EnemyController, ISendSignalToSelf
{
    [HideInInspector] public bool isStunned;
    public float elkStunTimer;
    private float stunTimer;

    private void Update()
    {
        EnemyStunned();
    }

    public void OnSignalReceived()
    {
        EndStun();
    }

    private void EnemyStunned()
    {
        if (isStunned)
        {
            if (stunTimer > 0) stunTimer -= Time.deltaTime;
            else EndStun();
        }
    }

    public void SetStun(float stunMaxTimer)
    {
        isStunned = true;
        stunTimer = stunMaxTimer;
    }

    public void EndStun()
    {
        isStunned = false;
    }
}

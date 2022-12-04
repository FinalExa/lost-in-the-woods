using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleShadeEnemyController : EnemyController, ISendSignalToSelf
{

    [HideInInspector] public bool isStunned;
    public float littleShadeStunTimer;
    private float stunTimer;

    private void Update()
    {
        EnemyStunned();
    }

    public void OnSignalReceived()
    {
        SetStun();
    }

    private void EnemyStunned()
    {
        if (isStunned)
        {
            if (stunTimer > 0) stunTimer -= Time.deltaTime;
            else EndStun();
        }
    }

    private void SetStun()
    {
        isStunned = true;
        stunTimer = littleShadeStunTimer;
    }

    public void EndStun()
    {
        isStunned = false;
        print("End");
    }
}

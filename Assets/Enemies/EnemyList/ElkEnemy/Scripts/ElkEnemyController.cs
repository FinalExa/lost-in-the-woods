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

    public void OnSignalReceived(GameObject source)
    {
        if (source != this.gameObject) EndStun(true);
        else SetStun(elkStunTimer);
    }

    private void EnemyStunned()
    {
        if (isStunned)
        {
            if (stunTimer > 0) stunTimer -= Time.deltaTime;
            else EndStun(false);
        }
    }

    public void SetStun(float stunMaxTimer)
    {
        isStunned = true;
        stunTimer = stunMaxTimer;
    }

    public void EndStun(bool redirect)
    {
        isStunned = false;
        if (redirect)
        {
            Vector3 pointDirection = (this.transform.position - playerTarget.transform.position);
            Vector3 endPoint = this.transform.position + pointDirection * 5f;
            enemyCombo.ActivateEnemyCombo(endPoint);
        }
    }
}

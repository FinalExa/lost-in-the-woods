using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleShadeEnemyController : EnemyController, ISendSignalToSelf, IHaveSpecialConditions
{

    [HideInInspector] public bool isStunned;
    private GenericNamedInteractionExecutor namedInteraction;
    public float littleShadeStunTimer;
    private float stunTimer;

    private void Update()
    {
        EnemyStunned();
    }

    protected override void Awake()
    {
        base.Awake();
        namedInteraction = this.gameObject.GetComponent<GenericNamedInteractionExecutor>();
    }

    protected override void Start()
    {
        base.Start();
        namedInteraction.enabled = false;
    }

    public void OnSignalReceived(GameObject source)
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
        namedInteraction.enabled = true;
        stunTimer = littleShadeStunTimer;
    }

    public void EndStun()
    {
        isStunned = false;
        namedInteraction.enabled = false;
    }

    public bool SpecialConditions()
    {
        return isStunned;
    }
}

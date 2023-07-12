using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchEnemyController : EnemyController, ISendSignalToSelf
{
    public WitchHidden witchHidden;
    public WitchLeap witchLeap;
    public WitchWeak witchWeak;
    public WitchCrying witchCrying;

    protected override void Awake()
    {
        base.Awake();
        witchHidden.SetController(this);
        witchLeap.SetController(this);
        witchWeak.SetController(this);
        witchCrying.SetController(this);
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    private void Start()
    {
        witchHidden.SetWitchHidden();
    }

    private void Update()
    {
        witchCrying.WitchCryingAction();
        witchLeap.LeapFinishTimer();
        witchHidden.LookAtPlayer();
    }

    public override void LightStateUpdate()
    {
        witchWeak.WitchWeakOperations();
        if (affectedByLight.lightState == AffectedByLight.LightState.BERSERK) witchHidden.LeaveHiddenState();
    }

    public void OnSignalReceived(GameObject source)
    {
        witchWeak.WitchWeakOperations();
    }
}

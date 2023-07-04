using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WitchEnemyController : EnemyController, ISendSignalToSelf
{
    public WitchLeap witchLeap;
    public WitchWeak witchWeak;
    public WitchCrying witchCrying;

    protected override void Awake()
    {
        base.Awake();
        witchLeap.SetController(this);
        witchWeak.SetController(this);
        witchCrying.SetController(this);
        interaction = this.gameObject.GetComponent<Interaction>();
    }

    private void Update()
    {
        witchCrying.WitchCryingAction();
        witchLeap.LeapFinishTimer();
    }

    public override void LightStateUpdate()
    {
        witchWeak.WitchWeakOperations();
    }

    public void OnSignalReceived(GameObject source)
    {
        witchWeak.WitchWeakOperations();
    }
}

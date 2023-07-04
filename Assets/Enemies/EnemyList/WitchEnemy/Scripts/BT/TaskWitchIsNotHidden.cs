using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskWitchIsNotHidden : Node
{
    private WitchEnemyController witchEnemyController;
    public TaskWitchIsNotHidden(WitchEnemyController _witchEnemyController)
    {
        witchEnemyController = _witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (witchEnemyController.witchHidden.GetIfWitchIsHidden()) return NodeState.FAILURE;
        return NodeState.SUCCESS;
    }
}

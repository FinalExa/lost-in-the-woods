using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskWitchIsWeak : Node
{
    private WitchEnemyController witchEnemyController;

    public TaskWitchIsWeak(WitchEnemyController _witchEnemyController)
    {
        witchEnemyController = _witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (witchEnemyController.witchWeak.GetIfWitchIsWeak()) return NodeState.FAILURE;
        return NodeState.SUCCESS;
    }
}

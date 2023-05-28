using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskWitchIsNotCrying : Node
{
    private WitchEnemyController witchEnemyController;
    public TaskWitchIsNotCrying(WitchEnemyController _witchEnemyController)
    {
        witchEnemyController = _witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (witchEnemyController.witchCrying) return NodeState.FAILURE;
        else return NodeState.SUCCESS;
    }
}

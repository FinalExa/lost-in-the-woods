using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskWitchWeakLeap : Node
{
    private WitchEnemyController witchEnemyController;
    public TaskWitchWeakLeap(WitchEnemyController _witchEnemyController)
    {
        witchEnemyController = _witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        witchEnemyController.DecideLeapObject();
        witchEnemyController.StartLeap();
        return NodeState.RUNNING;
    }
}

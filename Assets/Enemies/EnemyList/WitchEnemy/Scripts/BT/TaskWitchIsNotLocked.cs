using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskWitchIsNotLocked : Node
{
    private WitchEnemyController witchEnemyController;
    public TaskWitchIsNotLocked(WitchEnemyController _witchEnemyController)
    {
        witchEnemyController = _witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (witchEnemyController.witchCrying.GetIfWitchIsCrying() || witchEnemyController.witchLeap.GetIfWitchIsExecutingLeap()) return NodeState.FAILURE;
        else return NodeState.SUCCESS;
    }
}

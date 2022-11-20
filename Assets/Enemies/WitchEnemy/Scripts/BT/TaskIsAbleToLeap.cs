using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsAbleToLeapBack : Node
{
    WitchEnemyController _witchEnemyController;
    public TaskIsAbleToLeapBack(WitchEnemyController witchEnemyController)
    {
        _witchEnemyController = witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (_witchEnemyController.attackDone)
        {
            if (!_witchEnemyController.canLeap) SetUpLeap();
            return NodeState.FAILURE;
        }
        else return NodeState.SUCCESS;
    }

    private void SetUpLeap()
    {
        _witchEnemyController.DecideLeapObject();
        _witchEnemyController.canLeap = true;
    }
}

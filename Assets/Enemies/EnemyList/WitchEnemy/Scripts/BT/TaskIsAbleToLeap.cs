using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsAbleToLeap : Node
{
    WitchEnemyController _witchEnemyController;
    public TaskIsAbleToLeap(WitchEnemyController witchEnemyController)
    {
        _witchEnemyController = witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (_witchEnemyController.AttackDone)
        {
            _witchEnemyController.witchLeap.SetupLeap();
            return NodeState.FAILURE;
        }
        else return NodeState.SUCCESS;
    }
}

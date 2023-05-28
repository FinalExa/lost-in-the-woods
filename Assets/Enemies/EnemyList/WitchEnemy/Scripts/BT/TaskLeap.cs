using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskLeap : Node
{
    private WitchEnemyController _witchEnemyController;
    public TaskLeap(WitchEnemyController witchEnemyController)
    {
        _witchEnemyController = witchEnemyController;
    }

    public override NodeState Evaluate()
    {
        NodeState result = NodeState.SUCCESS;
        if (_witchEnemyController.canLeap && _witchEnemyController.AttackDone) LeapAction();
        return result;
    }

    private NodeState LeapAction()
    {
        float distance = Vector3.Distance(_witchEnemyController.transform.position, _witchEnemyController.leapDestination);
        if (distance > _witchEnemyController.leapTolerance)
        {
            _witchEnemyController.StartLeap();
            return NodeState.RUNNING;
        }
        else
        {
            _witchEnemyController.EndLeap();
            return NodeState.SUCCESS;
        }
    }
}

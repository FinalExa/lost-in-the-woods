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
        float distance = Vector3.Distance(_witchEnemyController.transform.position, _witchEnemyController.leapDestination);
        if (distance > _witchEnemyController.witchEnemyData.leapTolerance)
        {
            if (_witchEnemyController.thisNavMeshAgent.speed != _witchEnemyController.witchEnemyData.leapSpeed) _witchEnemyController.thisNavMeshAgent.speed = _witchEnemyController.witchEnemyData.leapSpeed;
            if (_witchEnemyController.thisNavMeshAgent.isStopped) _witchEnemyController.thisNavMeshAgent.isStopped = false;
            _witchEnemyController.thisNavMeshAgent.SetDestination(_witchEnemyController.leapDestination);
        }
        else
        {
            _witchEnemyController.attackDone = false;
            _witchEnemyController.canLeap = false;
        }
        return NodeState.RUNNING;
    }
}

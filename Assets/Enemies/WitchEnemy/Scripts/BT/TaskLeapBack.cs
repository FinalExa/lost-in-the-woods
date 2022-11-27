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
        if (_witchEnemyController.canLeap && _witchEnemyController.attackDone) LeapAction();
        return result;
    }

    private NodeState LeapAction()
    {
        float distance = Vector3.Distance(_witchEnemyController.transform.position, _witchEnemyController.leapDestination);
        if (distance > _witchEnemyController.witchEnemyData.leapTolerance)
        {
            if (_witchEnemyController.thisNavMeshAgent.speed != _witchEnemyController.witchEnemyData.leapSpeed) _witchEnemyController.thisNavMeshAgent.speed = _witchEnemyController.witchEnemyData.leapSpeed;
            if (_witchEnemyController.thisNavMeshAgent.isStopped) _witchEnemyController.thisNavMeshAgent.isStopped = false;
            _witchEnemyController.thisNavMeshAgent.SetDestination(_witchEnemyController.leapDestination);
            return NodeState.RUNNING;
        }
        else
        {
            _witchEnemyController.attackDone = false;
            _witchEnemyController.canLeap = false;
            return NodeState.SUCCESS;
        }
    }
}

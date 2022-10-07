using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskMoveToPlayerInsideLight : Node
{
    private EnemyController _enemyController;
    public TaskMoveToPlayerInsideLight(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_enemyController.transform.position, _enemyController.playerTarget.transform.position) >= _enemyController.lightUpDistance)
        {
            _enemyController.thisNavMeshAgent.speed = _enemyController.lightUpSpeed;
            if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
            _enemyController.thisNavMeshAgent.SetDestination(_enemyController.playerTarget.transform.position);
        }
        else _enemyController.thisNavMeshAgent.isStopped = true;
        state = NodeState.RUNNING;
        return state;
    }
}

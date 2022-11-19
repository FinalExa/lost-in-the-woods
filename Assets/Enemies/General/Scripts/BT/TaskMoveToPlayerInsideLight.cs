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
        _enemyController.thisNavMeshAgent.speed = _enemyController.enemyData.calmMovementSpeed;
        float distance = Vector3.Distance(_enemyController.transform.position, _enemyController.playerTarget.transform.position);
        if (distance >= _enemyController.enemyData.calmDistanceFromPlayer + _enemyController.enemyData.calmDistanceTolerance)
        {
            if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
            _enemyController.thisNavMeshAgent.SetDestination(_enemyController.playerTarget.transform.position);
        }
        else if (distance <= _enemyController.enemyData.calmDistanceFromPlayer - _enemyController.enemyData.calmDistanceTolerance)
        {
            if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
            Vector3 direction = _enemyController.playerTarget.gameObject.transform.position - _enemyController.gameObject.transform.position;
            Vector3 destination = -direction * 10f;
            _enemyController.thisNavMeshAgent.SetDestination(destination);
        }
        else _enemyController.thisNavMeshAgent.isStopped = true;
        state = NodeState.RUNNING;
        return state;
    }
}

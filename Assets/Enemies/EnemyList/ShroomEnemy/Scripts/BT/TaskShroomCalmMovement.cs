using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskShroomCalmMovement : Node
{
    private ShroomController _enemyController;
    public TaskShroomCalmMovement(EnemyController enemyController)
    {
        _enemyController = (ShroomController)enemyController;
    }

    public override NodeState Evaluate()
    {
        _enemyController.thisNavMeshAgent.speed = _enemyController.enemyData.calmMovementSpeed;
        float distance = Vector3.Distance(_enemyController.transform.position, _enemyController.playerTarget.transform.position);
        if (distance >= _enemyController.enemyData.calmDistanceFromPlayer + _enemyController.enemyData.calmDistanceTolerance) GoToPlayer();
        else if (distance <= _enemyController.enemyData.calmDistanceFromPlayer - _enemyController.enemyData.calmDistanceTolerance) EscapeFromPlayer();
        else _enemyController.thisNavMeshAgent.isStopped = true;
        state = NodeState.RUNNING;
        return state;
    }

    private void GoToPlayer()
    {
        if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
        _enemyController.thisNavMeshAgent.SetDestination(_enemyController.playerTarget.transform.position);
    }

    private void EscapeFromPlayer()
    {
        if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
        Vector3 direction = _enemyController.gameObject.transform.position - _enemyController.playerTarget.transform.position;
        direction = new Vector3(direction.x, 0f, direction.z);
        _enemyController.thisNavMeshAgent.SetDestination(_enemyController.transform.position + direction);
    }
}

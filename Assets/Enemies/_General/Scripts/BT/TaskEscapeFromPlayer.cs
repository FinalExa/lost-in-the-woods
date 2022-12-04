using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskEscapeFromPlayer : Node
{
    private EnemyController _enemyController;
    private float _movementSpeed;
    private float distanceFromPlayer;
    private float distanceToleranceFromPlayer;

    public TaskEscapeFromPlayer(EnemyController enemyController, float movementSpeed, float distance, float distanceTolerance)
    {
        _enemyController = enemyController;
        _movementSpeed = movementSpeed;
        distanceFromPlayer = distance;
        distanceToleranceFromPlayer = distanceTolerance;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.thisNavMeshAgent.speed != _movementSpeed) _enemyController.thisNavMeshAgent.speed = _movementSpeed;
        if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
        CheckIfDistantOrClose();
        state = NodeState.RUNNING;
        return state;
    }

    private void CheckIfDistantOrClose()
    {
        float distance = Vector3.Distance(_enemyController.thisNavMeshAgent.gameObject.transform.position, _enemyController.playerTarget.transform.position);
        if (distance > distanceFromPlayer + distanceToleranceFromPlayer) _enemyController.thisNavMeshAgent.SetDestination(_enemyController.playerTarget.transform.position);
        else
        {
            Vector3 direction = _enemyController.transform.position - _enemyController.playerTarget.transform.position;
            Vector3 destination = _enemyController.transform.position + (direction * 2f);
            _enemyController.thisNavMeshAgent.SetDestination(destination);
        }
    }
}
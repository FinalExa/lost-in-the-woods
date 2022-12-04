using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskEscapeFromPlayer : Node
{
    private EnemyController _enemyController;
    private float _movementSpeed;

    public TaskEscapeFromPlayer(EnemyController enemyController, float movementSpeed)
    {
        _enemyController = enemyController;
        _movementSpeed = movementSpeed;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.thisNavMeshAgent.speed != _movementSpeed) _enemyController.thisNavMeshAgent.speed = _movementSpeed;
        if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
        Vector3 direction = _enemyController.transform.position - _enemyController.playerTarget.transform.position;
        Vector3 destination = _enemyController.transform.position + (direction * 2f);
        _enemyController.thisNavMeshAgent.SetDestination(destination);
        state = NodeState.RUNNING;
        return state;
    }
}
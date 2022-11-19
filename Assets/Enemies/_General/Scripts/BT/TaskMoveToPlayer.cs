using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskMoveToPlayer : Node
{
    private EnemyController _enemyController;
    private float _movementSpeed;

    public TaskMoveToPlayer(EnemyController enemyController, float movementSpeed)
    {
        _enemyController = enemyController;
        _movementSpeed = movementSpeed;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.thisNavMeshAgent.speed != _movementSpeed) _enemyController.thisNavMeshAgent.speed = _movementSpeed;
        if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
        _enemyController.thisNavMeshAgent.SetDestination(_enemyController.playerTarget.transform.position);
        state = NodeState.RUNNING;
        return state;
    }
}
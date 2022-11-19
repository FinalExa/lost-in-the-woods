using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorTree;

public class TaskMoveToPlayer : Node
{
    private EnemyController _enemyController;

    public TaskMoveToPlayer(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public override NodeState Evaluate()
    {
        _enemyController.thisNavMeshAgent.speed = _enemyController.enemyData.normalMovementSpeed;
        if (_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = false;
        _enemyController.thisNavMeshAgent.SetDestination(_enemyController.playerTarget.transform.position);
        state = NodeState.RUNNING;
        return state;
    }
}
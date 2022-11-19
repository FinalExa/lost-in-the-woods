using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskStopMovement : Node
{
    private EnemyController _enemyController;
    public TaskStopMovement(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public override NodeState Evaluate()
    {
        if (!_enemyController.thisNavMeshAgent.isStopped) _enemyController.thisNavMeshAgent.isStopped = true;
        return NodeState.FAILURE;
    }
}

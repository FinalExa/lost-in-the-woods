using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsInsideLight : Node
{
    private EnemyController _enemyController;
    public TaskIsInsideLight(EnemyController enemyController)
    {
        _enemyController = enemyController;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.isInsideLight)
        {
            _enemyController.thisNavMeshAgent.isStopped = true;
            return NodeState.SUCCESS;
        }
        else return NodeState.FAILURE;
    }
}

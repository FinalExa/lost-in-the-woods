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
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}

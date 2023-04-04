using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBaloonNormalOperations : Node
{
    BaloonEnemyController baloonEnemyController;
    public TaskBaloonNormalOperations(BaloonEnemyController _baloonEnemycontroller)
    {
        baloonEnemyController = _baloonEnemycontroller;
    }

    public override NodeState Evaluate()
    {
        if (baloonEnemyController.enemyHealth.currentHP <= 0)
        {
            if (!baloonEnemyController.vulnerable) baloonEnemyController.HPDeplete();
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}

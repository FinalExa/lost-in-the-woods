using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEnemyIsNotLocked : Node
{
    private EnemyController enemyController;
    private EnemyCombo enemyCombo;
    public TaskEnemyIsNotLocked(EnemyController _enemyController)
    {
        enemyController = _enemyController;
        enemyCombo = enemyController.enemyCombo;
    }

    public override NodeState Evaluate()
    {
        if (enemyCombo.isInCombo || !enemyController.isAlerted) return NodeState.FAILURE;
        else return NodeState.SUCCESS;
    }
}
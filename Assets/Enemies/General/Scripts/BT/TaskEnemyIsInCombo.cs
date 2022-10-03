using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEnemyIsInCombo : Node
{
    private EnemyCombo enemyCombo;
    public TaskEnemyIsInCombo(EnemyCombo _enemyCombo)
    {
        enemyCombo = _enemyCombo;
    }

    public override NodeState Evaluate()
    {
        if (!enemyCombo.isInCombo) return NodeState.SUCCESS;
        else return NodeState.FAILURE;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private EnemyCombo enemyCombo;

    public TaskAttackPlayer(EnemyCombo _enemyCombo)
    {
        enemyCombo = _enemyCombo;
    }

    public override NodeState Evaluate()
    {
        enemyCombo.ActivateEnemyCombo();
        return NodeState.RUNNING;
    }
}

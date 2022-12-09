using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskAttackPlayer : Node
{
    private EnemyController enemyController;

    public TaskAttackPlayer(EnemyController _enemyController)
    {
        enemyController = _enemyController;
    }

    public override NodeState Evaluate()
    {
        enemyController.enemyCombo.ActivateEnemyCombo(enemyController.playerTarget.transform.position);
        return NodeState.RUNNING;
    }
}

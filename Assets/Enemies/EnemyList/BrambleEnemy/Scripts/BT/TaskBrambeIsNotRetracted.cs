using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBrambleIsNotRetracted : Node
{
    private BrambleController enemyController;
    public TaskBrambleIsNotRetracted(EnemyController _enemyController)
    {
        enemyController = (BrambleController)_enemyController;
    }

    public override NodeState Evaluate()
    {
        if (enemyController.isRetracted) return NodeState.FAILURE;
        else return NodeState.SUCCESS;
    }
}

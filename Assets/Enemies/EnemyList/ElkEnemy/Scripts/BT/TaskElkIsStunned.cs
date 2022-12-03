using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskElkIsStunned : Node
{
    ElkEnemyController elkEnemyController;
    public TaskElkIsStunned(ElkEnemyController _elkEnemyController)
    {
        elkEnemyController = _elkEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (elkEnemyController.isStunned) return NodeState.FAILURE;
        return NodeState.SUCCESS;
    }
}

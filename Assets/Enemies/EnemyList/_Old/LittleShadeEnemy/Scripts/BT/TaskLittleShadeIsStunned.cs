using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskLittleShadeIsStunned : Node
{
    private LittleShadeEnemyController littleShadeEnemyController;
    public TaskLittleShadeIsStunned(LittleShadeEnemyController _littleShadeEnemyController)
    {
        littleShadeEnemyController = _littleShadeEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (littleShadeEnemyController.isStunned) return NodeState.FAILURE;
        return NodeState.SUCCESS;
    }
}
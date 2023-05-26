using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskEntIsNotStunned : Node
{
    private EntEnemyController entEnemyController;
    public TaskEntIsNotStunned(EntEnemyController _entEnemyController)
    {
        entEnemyController = _entEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (entEnemyController.stunned) return NodeState.FAILURE;
        else return NodeState.SUCCESS;
    }
}

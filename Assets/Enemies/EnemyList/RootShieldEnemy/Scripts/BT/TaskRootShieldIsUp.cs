using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskRootShieldIsUp : Node
{
    private RootShieldEnemyController rootShieldEnemyController;
    public TaskRootShieldIsUp(RootShieldEnemyController _rootShieldEnemyController)
    {
        rootShieldEnemyController = _rootShieldEnemyController;
    }

    public override NodeState Evaluate()
    {
        if (rootShieldEnemyController.shieldUp) return NodeState.SUCCESS;
        return NodeState.FAILURE;
    }
}

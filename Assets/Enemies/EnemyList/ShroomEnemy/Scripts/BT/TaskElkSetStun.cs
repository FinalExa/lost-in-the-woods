using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskElkSetStun : Node
{
    ElkEnemyController elkEnemyController;
    public TaskElkSetStun(ElkEnemyController _elkEnemyController)
    {
        elkEnemyController = _elkEnemyController;
    }

    public override NodeState Evaluate()
    {
        elkEnemyController.SetStun(elkEnemyController.elkStunTimer);
        return NodeState.SUCCESS;
    }
}

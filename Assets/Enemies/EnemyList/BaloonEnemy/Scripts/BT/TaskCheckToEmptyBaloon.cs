using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskCheckToEmptyBaloon : Node
{
    BaloonEnemyController baloonEnemyController;
    public TaskCheckToEmptyBaloon(BaloonEnemyController _baloonEnemyController)
    {
        baloonEnemyController = _baloonEnemyController;
    }

    public override NodeState Evaluate()
    {
        baloonEnemyController.StopAbsorb();
        return NodeState.SUCCESS;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBaloonBerserkOperations : Node
{
    BaloonEnemyController baloonEnemyController;
    public TaskBaloonBerserkOperations(BaloonEnemyController _baloonEnemycontroller)
    {
        baloonEnemyController = _baloonEnemycontroller;
    }

    public override NodeState Evaluate()
    {
        if (baloonEnemyController.vulnerable ||
            baloonEnemyController.enemyHealth.currentHP <= 0 ||
            !baloonEnemyController.thisNavMeshAgent.enabled ||
            baloonEnemyController.enemyRotator.invertRotationLook ||
            !baloonEnemyController.grabbableByPlayer.lockedGrabbable) baloonEnemyController.HPReset();
        return NodeState.SUCCESS;
    }
}

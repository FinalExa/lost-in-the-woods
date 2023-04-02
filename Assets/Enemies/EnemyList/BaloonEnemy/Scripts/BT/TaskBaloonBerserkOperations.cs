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
        if (baloonEnemyController.vulnerable || baloonEnemyController.enemyHealth.currentHP <= 0)
        {
            baloonEnemyController.grabbableByPlayer.ReleaseFromBeingGrabbed(baloonEnemyController.playerRef.pcReferences.pcGrabbing);
            baloonEnemyController.HPReset();
        }
        return NodeState.FAILURE;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskBaloonCalmOperations : Node
{
    BaloonEnemyController baloonEnemyController;
    public TaskBaloonCalmOperations(BaloonEnemyController _baloonEnemycontroller)
    {
        baloonEnemyController = _baloonEnemycontroller;
    }
    public override NodeState Evaluate()
    {
        if (baloonEnemyController.enemyHealth.currentHP <= 0)
        {
            if (!baloonEnemyController.vulnerable) baloonEnemyController.HPDeplete();
            return NodeState.SUCCESS;
        }
        else if (baloonEnemyController.affectedByLight.lightState == AffectedByLight.LightState.CALM && !baloonEnemyController.vulnerable)
        {
            baloonEnemyController.enemyCombo.ActivateEnemyCombo(baloonEnemyController.playerTarget.transform.position);
            return NodeState.SUCCESS;
        }
        return NodeState.FAILURE;
    }
}

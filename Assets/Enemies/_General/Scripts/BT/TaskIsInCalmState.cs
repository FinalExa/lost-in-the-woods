using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsInCalmState : Node
{
    private EnemyController _enemyController;
    private EnemyWeaponSwitcher _enemyWeaponSwitcher;
    public TaskIsInCalmState(EnemyController enemyController, EnemyWeaponSwitcher enemyWeaponSwitcher)
    {
        _enemyController = enemyController;
        _enemyWeaponSwitcher = enemyWeaponSwitcher;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.affectedByLight.lightState == AffectedByLight.LightState.CALM)
        {
            _enemyController.CheckForSwitchState();
            if (_enemyController.enemyData.hasCalmWeapon) _enemyWeaponSwitcher.SetEnemyWeaponByState();
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}

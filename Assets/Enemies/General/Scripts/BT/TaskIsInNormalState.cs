using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsInNormalState : Node
{
    private EnemyController _enemyController;
    private EnemyWeaponSwitcher _enemyWeaponSwitcher;
    public TaskIsInNormalState(EnemyController enemyController, EnemyWeaponSwitcher enemyWeaponSwitcher)
    {
        _enemyController = enemyController;
        _enemyWeaponSwitcher = enemyWeaponSwitcher;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.enemyLightState == EnemyController.EnemyLightState.NORMAL)
        {
            if (_enemyController.enemyData.hasNormalWeapon) _enemyWeaponSwitcher.SetEnemyWeaponByState();
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}

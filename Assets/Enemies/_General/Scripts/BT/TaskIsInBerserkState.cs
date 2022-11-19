using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TaskIsInBerserkState : Node
{
    private EnemyController _enemyController;
    private EnemyWeaponSwitcher _enemyWeaponSwitcher;
    public TaskIsInBerserkState(EnemyController enemyController, EnemyWeaponSwitcher enemyWeaponSwitcher)
    {
        _enemyController = enemyController;
        _enemyWeaponSwitcher = enemyWeaponSwitcher;
    }

    public override NodeState Evaluate()
    {
        if (_enemyController.enemyLightState == EnemyController.EnemyLightState.BERSERK)
        {
            _enemyController.CheckForSwitchState();
            if (_enemyController.enemyData.hasBerserkWeapon) _enemyWeaponSwitcher.SetEnemyWeaponByState();
            state = NodeState.FAILURE;
            return state;
        }
        state = NodeState.SUCCESS;
        return state;
    }
}

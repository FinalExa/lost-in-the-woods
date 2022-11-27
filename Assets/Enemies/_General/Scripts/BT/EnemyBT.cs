using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public abstract class EnemyBT : BT_Tree
{
    [HideInInspector] public EnemyController enemyController;
    [HideInInspector] public EnemyWeaponSwitcher enemyWeaponSwitcher;

    protected virtual void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
        enemyWeaponSwitcher = this.gameObject.GetComponent<EnemyWeaponSwitcher>();
    }

    protected override Node SetupTree()
    {
        return new Node();
    }
}

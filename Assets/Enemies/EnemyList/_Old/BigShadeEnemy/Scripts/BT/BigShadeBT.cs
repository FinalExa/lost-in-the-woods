using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BigShadeBT : EnemyBT
{
    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskEnemyIsNotLocked(enemyController),
            new Selector(new List<Node>
            {
                new TaskIsInBerserkState(enemyController,enemyWeaponSwitcher),
                new TaskAttackPlayer(enemyController)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInNormalState(enemyController,enemyWeaponSwitcher),
                new TaskAttackPlayer(enemyController)
            })
        });
        return root;
    }
}

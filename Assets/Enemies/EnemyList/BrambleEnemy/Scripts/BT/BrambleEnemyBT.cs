using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class BrambleEnemyBT : EnemyBT
{
    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new Selector(new List<Node>
            {
                new TaskEnemyIsNotLocked(enemyController)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInBerserkState(enemyController, enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.berserkDistanceFromPlayer),
                    new TaskAttackPlayer(enemyController.enemyCombo)
                })
            }),
            new Selector(new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher)
            }),
            new Selector (new List<Node>
            {
                new TaskIsInNormalState(enemyController, enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.normalDistanceFromPlayer),
                    new TaskAttackPlayer(enemyController.enemyCombo)
                })
            })
        });
        return root;
    }
}

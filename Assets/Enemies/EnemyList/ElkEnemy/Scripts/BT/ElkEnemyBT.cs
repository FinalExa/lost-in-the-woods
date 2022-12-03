using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class ElkEnemyBT : EnemyBT
{
    private ElkEnemyController elkEnemyController;

    protected override void Awake()
    {
        base.Awake();
        elkEnemyController = (ElkEnemyController)enemyController;
    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskEnemyIsNotLocked(enemyController),
                new TaskElkIsStunned(elkEnemyController)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInBerserkState(enemyController,enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController,enemyController.enemyData.berserkDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.berserkMovementSpeed)
                    }),
                    new TaskAttackPlayer(enemyController.enemyCombo)
                })
            }),
            new Selector(new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher),
                new TaskStopMovement(enemyController)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInNormalState(enemyController,enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController,enemyController.enemyData.normalDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.normalMovementSpeed)
                    }),
                    new TaskElkSetStun(elkEnemyController),
                    new TaskAttackPlayer(enemyController.enemyCombo)
                })
            })
        });
        return root;
    }
}

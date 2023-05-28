using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class RootShieldEnemyBT : EnemyBT
{
    private RootShieldEnemyController rootShieldEnemyController;
    protected override void Awake()
    {
        base.Awake();
        rootShieldEnemyController = (RootShieldEnemyController)enemyController;
    }
    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskEnemyIsNotLocked(enemyController),
            new Selector(new List<Node>
            {
                new TaskIsInBerserkState(enemyController, enemyWeaponSwitcher),
                new Sequence (new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.berserkDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.berserkMovementSpeed),
                    }),
                    new TaskAttackPlayer(enemyController)
                })
            }),
            new Selector(new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new TaskRootShieldIsUp(rootShieldEnemyController),
                    new Sequence (new List<Node>
                    {
                        new Selector(new List<Node>
                        {
                            new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.calmDistanceFromPlayer),
                            new TaskMoveToPlayer(enemyController, enemyController.enemyData.calmMovementSpeed),
                        }),
                        new TaskAttackPlayer(enemyController)
                    })
                })
            }),
            new Selector (new List<Node>
            {
                new TaskIsInNormalState(enemyController, enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new TaskRootShieldIsUp(rootShieldEnemyController),
                    new Sequence (new List<Node>
                    {
                        new Selector(new List<Node>
                        {
                            new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.normalDistanceFromPlayer),
                            new TaskMoveToPlayer(enemyController, enemyController.enemyData.normalMovementSpeed),
                        }),
                        new TaskAttackPlayer(enemyController)
                    })
                })
            })
        });
        return root;
    }
}

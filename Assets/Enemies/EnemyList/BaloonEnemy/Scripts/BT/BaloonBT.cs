using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class BaloonBT : EnemyBT
{
    private BaloonEnemyController baloonEnemyController;

    protected override void Awake()
    {
        base.Awake();
        baloonEnemyController = (BaloonEnemyController)enemyController;
    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskEnemyIsNotLocked(enemyController),
            new TaskCheckToEmptyBaloon(baloonEnemyController),
            new Selector (new List<Node>
            {
                new TaskIsInBerserkState(enemyController, enemyWeaponSwitcher),
                new TaskBaloonBerserkOperations(baloonEnemyController),
                new Sequence(new List<Node>
                {
                    new Selector (new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.berserkDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.berserkMovementSpeed)
                    }),
                    new TaskAttackPlayer(enemyController)
                })
            }),
            new Selector (new List<Node>
            {
                new TaskIsInNormalState(enemyController, enemyWeaponSwitcher),
                new Selector(new List<Node>
                {
                    new TaskBaloonNormalOperations(baloonEnemyController),
                    new Sequence(new List<Node>
                    {
                        new Selector(new List<Node>
                        {
                            new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.normalDistanceFromPlayer),
                            new TaskMoveToPlayer(enemyController, enemyController.enemyData.normalMovementSpeed)
                        }),
                        new TaskAttackPlayer(enemyController)
                    })
                    }
                )
            }),
            new Selector (new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher),
                new Selector(new List<Node>
                {
                    new TaskBaloonCalmOperations(baloonEnemyController),
                    new TaskAttackPlayer(enemyController)
                })
            })
        });
        return root;
    }
}

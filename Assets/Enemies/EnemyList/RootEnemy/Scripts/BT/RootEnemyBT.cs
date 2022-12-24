using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class RootEnemyBT : EnemyBT
{
    [HideInInspector] public RootEnemyController rootEnemyController;

    protected override void Awake()
    {
        base.Awake();
        rootEnemyController = (RootEnemyController)enemyController;
    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new TaskEnemyIsNotLocked(enemyController),
            new TaskSetRootVulnerable(rootEnemyController),
            new Sequence (new List<Node>
            {
                new TaskIsInBerserkState(enemyController, enemyWeaponSwitcher),
                new Selector (new List<Node>
                {
                    new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.berserkDistanceFromPlayer),
                    new TaskMoveToPlayer(enemyController, enemyController.enemyData.berserkMovementSpeed)
                }),
                new TaskAttackPlayer(enemyController)
            }),
            new Sequence (new List<Node>
            {
                new TaskIsInNormalState(enemyController, enemyWeaponSwitcher),
                new Selector(new List<Node>
                {
                    new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.normalDistanceFromPlayer),
                    new TaskMoveToPlayer(enemyController, enemyController.enemyData.normalMovementSpeed)
                }),
                new TaskAttackPlayer(enemyController)
            }),
            new Sequence (new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher),
                new TaskStopMovement(enemyController)
            })
        });
        return root;
    }
}

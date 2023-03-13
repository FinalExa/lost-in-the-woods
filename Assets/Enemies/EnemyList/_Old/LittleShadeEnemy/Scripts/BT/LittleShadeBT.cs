using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
public class LittleShadeBT : EnemyBT
{
    private LittleShadeEnemyController littleShadeEnemyController;

    protected override void Awake()
    {
        base.Awake();
        littleShadeEnemyController = (LittleShadeEnemyController)enemyController;
    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskEnemyIsNotLocked(enemyController),
                new TaskLittleShadeIsStunned(littleShadeEnemyController)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInBerserkState(enemyController,enemyWeaponSwitcher),
                new Sequence(new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new TaskIsFarFromPlayer(enemyController, enemyController.enemyData.berserkDistanceFromPlayer, enemyController.enemyData.berserkDistanceTolerance),
                        new TaskEscapeFromPlayer(enemyController, enemyController.enemyData.berserkMovementSpeed, enemyController.enemyData.berserkDistanceFromPlayer, enemyController.enemyData.berserkDistanceTolerance)
                    }),
                    new TaskAttackPlayer(enemyController)
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
                        new TaskIsFarFromPlayer(enemyController,enemyController.enemyData.normalDistanceFromPlayer, enemyController.enemyData.normalDistanceTolerance),
                        new TaskEscapeFromPlayer(enemyController, enemyController.enemyData.normalMovementSpeed, enemyController.enemyData.normalDistanceFromPlayer, enemyController.enemyData.normalDistanceTolerance)
                    }),
                    new TaskAttackPlayer(enemyController)
                })
            })
        });
        return root;
    }
}

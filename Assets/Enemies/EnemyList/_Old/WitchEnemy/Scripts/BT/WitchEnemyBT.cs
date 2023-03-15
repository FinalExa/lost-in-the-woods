using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WitchEnemyBT : EnemyBT
{
    [HideInInspector] public WitchEnemyController witchEnemyController;

    protected override void Awake()
    {
        base.Awake();
        witchEnemyController = this.gameObject.GetComponent<WitchEnemyController>();
    }

    protected override Node SetupTree()
    {
        Node root = new Sequence(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new TaskLeap(witchEnemyController),
                new TaskEnemyIsNotLocked(enemyController)
            }),
            new Selector(new List<Node>
            {
                new TaskIsInBerserkState(enemyController, enemyWeaponSwitcher),
                new Sequence (new List<Node>
                {
                    new TaskIsAbleToLeapBack(witchEnemyController),
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
                new TaskStopMovement(enemyController),
                new TaskAttackPlayer(enemyController)
            }),
            new Selector (new List<Node>
            {
                new TaskIsInNormalState(enemyController, enemyWeaponSwitcher),
                new Sequence (new List<Node>
                {
                    new TaskIsAbleToLeapBack(witchEnemyController),
                    new Selector(new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.normalDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.normalMovementSpeed),
                    }),
                    new TaskAttackPlayer(enemyController)
                })
            })
        });
        return root;
    }
}

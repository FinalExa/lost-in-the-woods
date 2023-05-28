using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class WitchEnemyBT : EnemyBT
{
    private WitchEnemyController witchEnemyController;

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
                new TaskEnemyIsNotLocked(enemyController),
                new TaskWitchIsNotCrying(witchEnemyController),
                new Selector(new List<Node>
                {
                    new TaskWitchIsWeak(witchEnemyController),
                    new TaskWitchWeakLeap(witchEnemyController)
                }),
                new TaskLeap(witchEnemyController)
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
            new TaskIsInCalmState(enemyController, enemyWeaponSwitcher),
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

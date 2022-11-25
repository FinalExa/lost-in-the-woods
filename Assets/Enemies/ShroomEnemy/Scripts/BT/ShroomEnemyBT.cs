using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class ShroomEnemyBT : BT_Tree
{
    [HideInInspector] public EnemyController enemyController;
    [HideInInspector] public EnemyWeaponSwitcher enemyWeaponSwitcher;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
        enemyWeaponSwitcher = this.gameObject.GetComponent<EnemyWeaponSwitcher>();
    }

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
                new Sequence (new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.berserkDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.berserkMovementSpeed),
                    }),
                    new TaskAttackPlayer(enemyController.enemyCombo)
                })
            }),
            new Selector(new List<Node>
            {
                new TaskIsInCalmState(enemyController, enemyWeaponSwitcher),
                new TaskShroomCalmMovement(enemyController)
            }),
            new Selector (new List<Node>
            {
                new TaskIsInNormalState(enemyController, enemyWeaponSwitcher),
                new Sequence (new List<Node>
                {
                    new Selector(new List<Node>
                    {
                        new TaskIsCloseToPlayer(enemyController, enemyController.enemyData.normalDistanceFromPlayer),
                        new TaskMoveToPlayer(enemyController, enemyController.enemyData.normalMovementSpeed),
                    }),
                    new TaskAttackPlayer(enemyController.enemyCombo)
                })
            })
        });
        return root;
    }
}

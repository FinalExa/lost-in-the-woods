using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class TestEnemyBT : BT_Tree
{
    [HideInInspector] public EnemyController enemyController;

    private void Awake()
    {
        enemyController = this.gameObject.GetComponent<EnemyController>();
    }

    protected override Node SetupTree()
    {

        Node root = new Sequence(new List<Node>
        {
            new Selector(new List<Node>
            {
                new TaskEnemyIsInCombo(enemyController.enemyCombo)
            }),
            new Selector(new List<Node>
            {
                new TaskIsCloseToPlayer(enemyController.thisNavMeshAgent, enemyController.playerTarget, enemyController.attackDistance),
                new TaskMoveToPlayer(enemyController.thisNavMeshAgent, enemyController.playerTarget),
            }),
            new Selector(new List<Node>
            {
                new TaskAttackPlayer(enemyController.enemyCombo)
            })
        });
        return root;
    }
}

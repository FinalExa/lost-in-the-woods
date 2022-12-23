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
            new TaskSetInvulnerability(enemyController,rootEnemyController.rootUnderground),
        });
        return root;
    }
}
